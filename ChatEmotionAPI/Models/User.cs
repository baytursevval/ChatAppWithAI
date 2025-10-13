using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ChatEmotionAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nickname { get; set; }

        [JsonIgnore]
        public ICollection<Message>? SentMessages { get; set; }

        [JsonIgnore]
        public ICollection<Message>? ReceivedMessages { get; set; }
    }
}
