using Microsoft.AspNetCore.Mvc;
using WukongDemo.project.Models;
using WukongDemo.project.Service;
using WukongDemo.Util;

namespace WukongDemo.project.Controller
{
    public class ProjectMemberController
    {
        [Route("api/projects")]
        [ApiController]
        public class ProjectController : ControllerBase
        {
            private readonly ProjectMemberService _projectMemberService;
            private readonly ProjectService _projectService;

            public ProjectController(ProjectMemberService projectMemberService, ProjectService projectService)
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
                    return NotFound(new { success = false, message = "No members found for this project." });
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
                var result = await _projectMemberService.AddProjectMemberAsync(projectId, userId, request.NewMemberId, request.Role);

                switch (result)
                {                   
                    case "Forbidden":
                        return Forbid("Access Denied");
                    case "AlreadyMember":
                        return BadRequest(new { success = false, message = "User is already a member of the project." });
                    case "MemberAdded":
                        return Ok(new { success = true, message = "Member added successfully." });
                    default:
                        return StatusCode(500, new { success = false, message = "An unexpected error occurred." });
                }
            }

            /// <summary>
            /// 变更项目成员身份
            /// </summary>
            [HttpPut("{projectId}/members/{userId}")]
            public async Task<IActionResult> ChangeMemberRole([FromRoute] int projectId,[FromRoute] int updateId,[FromBody] string newRole, [FromHeader] string authorization)
            {
                int userId = AuthUtils.GetUserIdFromToken(authorization);

                var result = await _projectMemberService.ChangeMemberRoleAsync(projectId, userId, newRole, updateId);

                if (result == "Forbidden")
                {
                    return Forbid("You do not have permission to change this member's role.");
                }
                if (result == "NotFound")
                {
                    return NotFound(new { message = "Member not found in the project." });
                }

                return Ok(new { message = "Member role updated successfully." });
            }

            /// <summary>
            /// 删除项目成员
            /// </summary>
            [HttpDelete("{userId}")]
            public async Task<IActionResult> RemoveProjectMember([FromRoute] int projectId,[FromRoute] int deleteId,[FromHeader] string authorization)  // 通过 Authorization Header 传递 Token
            {
                int userId = AuthUtils.GetUserIdFromToken(authorization);

                var result = await _projectMemberService.RemoveProjectMemberAsync(projectId, userId, deleteId);

                if (result == "Forbidden")
                {
                    return Forbid("You do not have permission to remove this member.");
                }
                if (result == "NotFound")
                {
                    return NotFound(new { message = "Member not found in the project." });
                }

                return Ok(new { message = "Member removed successfully." });
            }
        }
    }
}
