using WukongDemo.inAppMessage.Models;
using WukongDemo.project.Models;
using WukongDemo.joinRequest.Models;

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
        public ICollection<InAppMessage> SentMessages { get; set; }
        public ICollection<InAppMessage> ReceivedMessages { get; set; }
        public ICollection<ProjectMember> ProjectMembers { get; set; }
        public ICollection<JoinRequest> JoinRequestsAsApplicant {  get; set; }
        public ICollection<JoinRequest> JoinRequestsAsReviewer { get; set; }
    }
}
