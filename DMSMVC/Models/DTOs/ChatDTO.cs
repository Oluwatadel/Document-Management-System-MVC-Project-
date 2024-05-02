using DMSMVC.Models.Entities;

namespace DMSMVC.Models.DTOs
{
    public class ChatDTO
    {
        public Guid Id { get; set; }
        public string SenderEmail { get; set; }
        public string ReceiverEmail { get; set; }
        public User User { get; set; }
        public ICollection<ChatContent> ChatContents { get; set; } = new List<ChatContent>();
    }
}
