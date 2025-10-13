using ChatEmotionAPI.Data;
using ChatEmotionAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace ChatEmotionAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;
        //private readonly string _aiServiceUrl = "http://localhost:5285/";

        public MessageController(AppDbContext context)
        {
            _context = context;
            _httpClient = new HttpClient();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] Message message)
        {
            try
            {
                message.CreatedAt = DateTime.UtcNow;

                var payload = new { text = message.Text };

                var aiUrl = "https://sevvaltzl-sentimentt.hf.space/api/predict";
                var response = await _httpClient.PostAsJsonAsync(aiUrl, payload);

                var responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine("AI raw response: " + responseString);

                string sentiment = "unknown";
                double score = 0;

                if (response.IsSuccessStatusCode)
                {
                    using var jsonDoc = JsonDocument.Parse(responseString);
                    sentiment = jsonDoc.RootElement.GetProperty("sentiment").GetString() ?? "unknown";
                    score = jsonDoc.RootElement.GetProperty("score").GetDouble();
                }
                else
                {
                    Console.WriteLine($"AI service returned {response.StatusCode}");
                }

                message.Sentiment = sentiment;
                message.Score = score;

                _context.Messages.Add(message);
                await _context.SaveChangesAsync();

                return Ok(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("MessageController error: " + ex.Message);
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages([FromQuery] int? user1, [FromQuery] int? user2)
        {
            IQueryable<Message> query = _context.Messages;

            if (user1.HasValue && user2.HasValue)
            {
                query = query.Where(m =>
                    (m.SenderId == user1 && m.ReceiverId == user2) ||
                    (m.SenderId == user2 && m.ReceiverId == user1)
                );
            }

            var messages = await query
                .OrderBy(m => m.CreatedAt)
                .ToListAsync();

            return Ok(messages);
        }
    }
}
