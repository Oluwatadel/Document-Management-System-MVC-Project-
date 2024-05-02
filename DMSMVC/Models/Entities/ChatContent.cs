using System.Text.Json;

namespace DMSMVC.Models.Entities
{
    public class ChatContent : Base
    {
        public string ChatContentReference { get; set; } = Guid.NewGuid().ToString();
        public string? ContentOfChat { get; set; }
        public Guid ChatId { get; set; }
        public Chat Chat { get; set; } = default!;
        //public string ChatContentSenderEmail { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    }
}
