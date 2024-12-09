using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WukongDemo.joinRequest.Models;
using WukongDemo.user.Models;

namespace WukongDemo.project.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string ProjectType { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }

        public int LeaderId { get; set; }
        public int InstructorId { get; set; }      
        public int MaxMembers { get; set; }
        public int CurrentMembers { get; set; }

        // 导航属性
        [JsonIgnore]
        public ICollection<ProjectMember> ProjectMembers { get; set; }
        [JsonIgnore]
        public User Leader { get; set; }
        [JsonIgnore]
        public User Instructor { get; set; }
        [JsonIgnore]
        public ICollection<JoinRequest> JoinRequests { get; set; }
    }
}
