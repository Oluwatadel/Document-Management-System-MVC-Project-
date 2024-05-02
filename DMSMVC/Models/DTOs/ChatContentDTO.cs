using DMSMVC.Models.Entities;

namespace DMSMVC.Models.DTOs
{
    public class ChatContentDTO
    {
        public string ChatContentReference { get; set; } = Guid.NewGuid().ToString();
        public string? ContentOfChat { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
