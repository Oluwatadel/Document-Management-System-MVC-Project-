namespace DMSMVC.Models.RequestModel
{
    public class ChatContentRequest
    {
        public Guid Id { get; set; }
        public string ChatContentId { get; set; } = Guid.NewGuid().ToString();
        public string? ContentOfChat { get; set; }
    }
}
