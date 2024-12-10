using WukongDemo.user.Models;
using WukongDemo.project.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WukongDemo.inAppMessage.Models
{
    /// <summary>
    /// 站内信定义
    /// </summary>
    public class InAppMessage
    {
        public int InAppMessageId { get; set; }
        public required int SenderId { get; set; }
        public required int RecipientId { get; set; }
        public required int Type { get; set; }
        public required string Subject { get; set; }
        public required string Content { get; set; }
        public DateTime SentAt { get; set; }
        public required bool IsRead { get; set; }
        public int? RelatedProjectId { get; set; }


        // 导航属性
        [JsonIgnore]
        [ForeignKey("SenderId")]
        public User Sender { get; set; }

        [JsonIgnore]
        [ForeignKey("RecipientId")]
        public User Recipient { get; set; }

        [JsonIgnore]
        [ForeignKey("RelatedProjectId")]
        public Project RelatedProject { get; set; }
    }
}