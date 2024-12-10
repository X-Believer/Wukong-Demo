using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WukongDemo.project.Models;
using WukongDemo.user.Models;

namespace WukongDemo.joinRequest.Models
{
    public class JoinRequest
    {
        public int JoinRequestId { get; set; }
      
        public int ProjectId { get; set; }

        public int ApplicantId { get; set; }

        public required string Type { get; set; }

        public string Reason { get; set; }

        public string SelfIntroduction { get; set; }

        public string Status { get; set; }

        public DateTime SubmittedAt { get; set; }

        public int? ReviewedBy { get; set; }

        public DateTime? ReviewedAt { get; set; }

        // 导航属性
        [JsonIgnore]
        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

        [ForeignKey("ApplicantId")]
        public User Applicant { get; set; }

        [ForeignKey("ReviewedBy")]
        public User Reviewer { get; set; }

    }

}
