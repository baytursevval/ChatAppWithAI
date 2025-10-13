using ChatEmotionAPI.Data;
using ChatEmotionAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var existing = await _context.Users.FirstOrDefaultAsync(u => u.Nickname == user.Nickname);
            if (existing != null)
                return Ok(existing);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User loginRequest)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Nickname == loginRequest.Nickname);

            if (user == null)
                return NotFound(new { message = "Kullanıcı bulunamadı." });

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetUsers() => Ok(_context.Users.ToList());
    }
}
