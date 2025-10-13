using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatEmotionAPI.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public string? Sentiment { get; set; }

        public double Score { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign keys
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

        // Navigation properties
        [ForeignKey(nameof(SenderId))]
        public User? Sender { get; set; }

        [ForeignKey(nameof(ReceiverId))]
        public User? Receiver { get; set; }
    }
}
