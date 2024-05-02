using DMSMVC.Models.Entities;

namespace DMSMVC.Models.ViewModels
{
    public class ChatDisplayModel
    {
        public Guid Id { get; set; }
        public string? SenderEmail { get; set; }
        public string? SenderFullName { get; set; }
        public string? SenderPics { get; set; }
        public string? ReceiverPics { get; set; }
        public string? ReceiverEmail { get; set; }
        public string? ReceiverFullName{ get; set; }
        //public string? ChatReference { get; set; } = Guid.NewGuid().ToString().Substring(0, 6);
        public ICollection<ChatContent> ChatContents { get; set; } = new HashSet<ChatContent>();
    }
}
