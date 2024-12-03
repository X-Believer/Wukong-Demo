using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using WukongDemo.Data;
using WukongDemo.recruitment.Models;
using WukongDemo.recruitment.Services;

namespace WukongDemo.recruitment.Controllers
{
    [ApiController]
    [Route("/projects/{id}/recruitment")]
    public class RecruitmentController : ControllerBase
    {
        private readonly IRecruitmentService _recruitmentService;
        private readonly AppDbContext _context;

        public RecruitmentController(AppDbContext context, IRecruitmentService recruitmentService)
        {
            _recruitmentService = recruitmentService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetRecruitmentInfo(int id)
        {
            var project = await _context.Projects
                .Include(p => p.Members)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                return NotFound(new { message = "项目不存在" });
            }

            var recruitmentInfo = new
            {
                projectId = project.Id,
                title = project.Title,
                description = project.Description,
                status = project.Status,
                isRecruiting = project.Status == "招募中",
                maxMembers = project.MaxMembers,
                currentMembers = project.CurrentMembers,
                remainingSpots = project.MaxMembers - project.CurrentMembers,
                leaderId = project.LeaderId,
                memberIds = project.Members.Select(m => m.Id).ToList()
            };

            return Ok(recruitmentInfo);
        }
    }
}
