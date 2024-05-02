namespace DMSMVC.Models.RequestModel
{
    public class ChatRequestModel
    {
        public Guid Id { get; set; }
        public string SenderEmail { get; set; } = default!;
        public string ReceiverEmail { get; set; } = default!;
        public string? Message { get; set; } = default!;
    }
}
