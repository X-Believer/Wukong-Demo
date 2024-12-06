using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WukongDemo.project.Models;
using WukongDemo.project.Service;
namespace WukongDemo.project.Controller
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _projectService;

        public ProjectController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        /// <summary>
        /// 获取项目负责人
        /// </summary>
        [HttpGet("{projectId}/leader")]
        public async Task<IActionResult> GetProjectLeader(int projectId)
        {
            var leader = await _projectService.GetProjectLeaderAsync(projectId);

            if (leader == null)
            {
                return NotFound(new { success = false, message = "Project leader not found." });
            }

            return Ok(new { success = true, data = leader });
        }
    }
}
