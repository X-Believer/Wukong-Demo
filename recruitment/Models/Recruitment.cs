namespace WukongDemo.recruitment.Models
{
    public class Recruitment
    {
        public int Id { get; set; } // 招募信息 ID
        public int ProjectId { get; set; } // 所属项目 ID
        public int ExpectedMembers { get; set; } // 期望招募人数
        public int RecruitedMembers { get; set; } = 0; // 已招募人数
        public string RoleDescription { get; set; } // 角色描述
        public string Requirements { get; set; } // 要求
        public int RecruiterId { get; set; } // 发布者用户 ID
        public string Status { get; set; } = "open"; // 状态（open/closed）
        public DateTime CreatedAt { get; set; } = DateTime.Now; // 创建时间
        public DateTime UpdatedAt { get; set; } = DateTime.Now; // 更新时间

        // 检查是否还能继续招募
        public bool CanRecruit() => RecruitedMembers < ExpectedMembers;
    }
}
