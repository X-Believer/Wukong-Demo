namespace WukongDemo.inAppMessage.Models
{
    public class SendMessageRequest
    {
        public required int RecipientId { get; set; }
        public required int RelatedProjectId { get; set; }
        public required int Type { get; set; }
        public required string Subject { get; set; }
        public required string Content { get; set; }
    }
}
