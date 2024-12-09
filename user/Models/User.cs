using WukongDemo.inAppMessage.Models;
using WukongDemo.project.Models;
using WukongDemo.joinRequest.Models;
using System.Text.Json.Serialization;

namespace WukongDemo.user.Models
{
    /// <summary>
    /// 用户定义
    /// </summary>
    public class User
    {
        public required int UserId { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }

        // 导航属性
        [JsonIgnore]
        public ICollection<InAppMessage> SentMessages { get; set; }
        [JsonIgnore]
        public ICollection<InAppMessage> ReceivedMessages { get; set; }
        [JsonIgnore]
        public ICollection<ProjectMember> ProjectMembers { get; set; }
        [JsonIgnore]
        public ICollection<JoinRequest> JoinRequestsAsApplicant {  get; set; }
        [JsonIgnore]
        public ICollection<JoinRequest> JoinRequestsAsReviewer { get; set; }
    }
}
