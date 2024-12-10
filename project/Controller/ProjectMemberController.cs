using Microsoft.AspNetCore.Mvc;
using WukongDemo.project.Models;
using WukongDemo.project.Service;
using WukongDemo.Util;

namespace WukongDemo.project.Controller
{
    [Route("projects")]
    [ApiController]
    public class ProjectMemberController : ControllerBase
    {
        
        private readonly ProjectMemberService _projectMemberService;
        private readonly ProjectService _projectService;

        public ProjectMemberController(ProjectMemberService projectMemberService, ProjectService projectService)
        {
            _projectService = projectService;
            _projectMemberService = projectMemberService;
        }

        /// <summary>
        /// 获取项目的全部成员
        /// </summary>
        [HttpGet("{projectId}/members")]
        public async Task<IActionResult> GetProjectMembers(int projectId)
        {
            var projectMembers = await _projectMemberService.GetProjectMembersAsync(projectId);

            if (projectMembers == null || !projectMembers.Any())
            {
                return NotFound(new { errorCode = 404, success = false, message = "No members found for this project." });
            }

            // 返回项目成员信息
            return Ok(new { success = true, data = projectMembers });
        }

        /// <summary>
        /// 添加项目成员
        /// </summary>
        [HttpPost("{projectId}/members")]
        public async Task<IActionResult> AddProjectMember(int projectId, [FromHeader] string authorization, [FromBody] AddMemberRequest request)
        {
            var userId = AuthUtils.GetUserIdFromToken(authorization);  // 从 Token 中获取用户 ID            

            // 调用 Service 层方法添加项目成员
            try
            {
                var addRequest = await _projectMemberService.AddProjectMemberAsync(projectId, userId, request.NewMemberId, request.Role);

                return Ok(new
                {
                    success = true,
                    message = "添加成员成功",
                    data = addRequest
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { errorCode=401, success = false, message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { errorCode = 404, success = false, message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { errorCode=400, success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }                   
        }

        /// <summary>
        /// 变更项目成员身份
        /// </summary>
        [HttpPut("{projectId}/members/{updateId}")]
        public async Task<IActionResult> ChangeMemberRole([FromRoute] int projectId, [FromRoute] int updateId, [FromQuery] string newRole, [FromHeader] string authorization)
        {
            int userId = AuthUtils.GetUserIdFromToken(authorization);

            try
            {
                var result = await _projectMemberService.ChangeMemberRoleAsync(projectId, userId, newRole, updateId);
                return Ok(new { message = result });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { errorCode=401, success = false, message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { errorCode=404, success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// 删除项目成员
        /// </summary>
        [HttpDelete("{projectId}/members/{deleteId}")]
        public async Task<IActionResult> RemoveProjectMember([FromRoute] int projectId, [FromRoute] int deleteId, [FromHeader] string authorization)  // 通过 Authorization Header 传递 Token
        {
            int userId = AuthUtils.GetUserIdFromToken(authorization);

            try
            {
                var result = await _projectMemberService.RemoveProjectMemberAsync(projectId, userId, deleteId);
                return Ok(new { message = result });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { errorCode=401, success = false, message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { errorCode=404, success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        
    }
}
