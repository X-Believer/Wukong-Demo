using WukongDemo.user.Models;
using WukongDemo.project.Models;

namespace WukongDemo.inAppMessage.Models
{
    /// <summary>
    /// 站内信定义
    /// </summary>
    public class InAppMessage
    {
        public int Id { get; set; }
        public required int SenderId { get; set; }
        public required int RecipientId { get; set; }
        public required int Type { get; set; }
        public required string Subject { get; set; }
        public required string Content { get; set; }
        public DateTime SentAt { get; set; }
        public required bool IsRead { get; set; }
        public int? RelatedProjectId { get; set; }

        // 导航属性
        public User Sender { get; set; }
        public User Recipient { get; set; }
        public Project RelatedProject { get; set; }
    }
}