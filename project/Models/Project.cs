using WukongDemo.user.Models;

namespace WukongDemo.project.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string ProjectType { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Status { get; set; }

        public string LeaderId { get; set; }
        public User Leader { get; set; }

        public string InstructorId { get; set; }
        public User Instructor { get; set; }

        public int MaxMembers { get; set; }
        public int CurrentMembers { get; set; }

        // 导航属性
        public ICollection<ProjectMember> ProjectMembers { get; set; }
        
    }
}
