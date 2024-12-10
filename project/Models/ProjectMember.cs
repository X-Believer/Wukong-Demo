using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WukongDemo.user.Models;

namespace WukongDemo.project.Models
{
    public class ProjectMember
    {
        public int ProjectMemberId { get; set; }
        public required int ProjectId { get; set; }
        public required int UserId { get; set; }
        public required string Role { get; set; }        // 项目内角色（如指导老师、负责人、普通成员）
        public DateTime JoinDate { get; set; }
        public required string Status { get; set; }      // 状态（如：在职、已退出）

        // 导航属性
        [JsonIgnore]
        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

        [JsonIgnore]
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
