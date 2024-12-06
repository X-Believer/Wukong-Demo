using Microsoft.AspNetCore.Mvc;
using System.Net;
using WukongDemo.Data;
using WukongDemo.joinRequest.Models;
using WukongDemo.joinRequest.Service;
using WukongDemo.Util;

namespace WukongDemo.joinRequest.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class JoinRequestController : ControllerBase
    {
        private readonly JoinRequestService _joinRequestService;

        public JoinRequestController(JoinRequestService joinRequestService)
        {
            _joinRequestService = joinRequestService;
        }

        /// <summary>
        /// 获取某一项目的全部加入申请
        /// </summary>        
        [HttpGet("projects/{projectId}/join-requests")]
        public async Task<IActionResult> GetJoinRequestsByProjectId(int projectId, int pageNumber = 1, int pageSize = 10)
        {
            // 调用 Service 层获取项目的所有加入申请
            var (joinRequests, totalCount) = await _joinRequestService.GetJoinRequestsByProjectIdAsync(projectId, pageNumber, pageSize);

            if (joinRequests == null || !joinRequests.Any())
            {
                return NotFound(new { message = "No join requests found for this project." });
            }

            // 计算总页数
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var response = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                PageNumber = pageNumber,
                PageSize = pageSize,
                JoinRequests = joinRequests
            };

            return Ok(response);
        }

        /// <summary>
        /// 获取项目的某一加入申请
        /// </summary>        
        [HttpGet("join-requests/{id}")]
        public async Task<IActionResult> GetJoinRequestById([FromRoute] int id)
        {
            var joinRequest = await _joinRequestService.GetJoinRequestByIdAsync(id);

            if (joinRequest == null)
            {
                return NotFound(new { errorCode = "404", message = "Join request not found." });
            }

            return Ok(joinRequest);
        }

        /// <summary>
        /// 发送加入申请
        /// </summary>        
        [HttpPost("join-request")]
        public async Task<IActionResult> SendJoinRequest([FromHeader] string authorization, [FromBody] JoinRequestDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new { errorCode = "400", message = "Invalid request data." });
            }

            var applicantId = AuthUtils.GetUserIdFromToken(authorization);

            var joinRequest = await _joinRequestService.SendJoinRequestAsync(dto.ProjectId, applicantId, dto.Type, dto.Reason, dto.SelfIntroduction);

            return CreatedAtAction(nameof(GetJoinRequestById), new { id = joinRequest.JoinRequestId }, joinRequest);
        }

        /// <summary>
        /// 修改加入申请
        /// </summary>
        [HttpPut("join-requests/{id}")]
        public async Task<IActionResult> UpdateJoinRequest([FromHeader] string authorization, [FromRoute] int id, [FromBody] JoinRequestDto updateDto)
        {
            var userId = AuthUtils.GetUserIdFromToken(authorization);
            try
            {
                // 调用 Service 层方法修改加入申请
                var updatedRequest = await _joinRequestService.UpdateJoinRequestAsync(id, userId, updateDto.Type, updateDto.Reason, updateDto.SelfIntroduction);

                // 返回修改后的申请信息
                return Ok(new
                {
                    success = true,
                    message = "申请修改成功",
                    data = updatedRequest
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// 审核加入申请
        /// </summary>
        [HttpPut("review-join-requests/{id}")]
        public async Task<IActionResult> ApproveJoinRequestAsync([FromRoute] int requestId, [FromHeader] string authorization, [FromQuery] bool isApproved)
        {
            try
            {
                var userId = AuthUtils.GetUserIdFromToken(authorization);

                var result = await _joinRequestService.ApproveJoinRequestAsync(requestId, userId, isApproved);

                if (result == "Success")
                {
                    return Ok(new { success = true, message = "申请已成功审核" });
                }
                else if (result == "JoinRequestNotFound")
                {
                    return NotFound(new { error = "NotFound" });
                }
                else if (result == "Forbidden")
                {
                    return Forbid("Access Denied");
                }
                else if (result == "MaxMembersReached")
                {
                    return BadRequest(new { error = "项目成员已满，无法添加新成员" });
                }
                else if (result == "Rejected")
                {
                    return BadRequest(new { error = "加入申请已被拒绝" });
                }
                else if (result == "MemberAlreadyExists")
                {
                    return BadRequest(new { error = "该成员已经是项目成员" });
                }
                return StatusCode(500, new { error = "审核失败，系统错误" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "服务器内部错误", details = ex.Message });
            }
        }

        /// <summary>
        /// 删除加入申请
        /// </summary>
        [HttpDelete("join-requests/{id}")]
        public async Task<IActionResult> DeleteJoinRequest([FromHeader] string authorization,  [FromRoute] int id)
        {
            var userId = AuthUtils.GetUserIdFromToken(authorization);

            try
            {
                var result = await _joinRequestService.DeleteJoinRequestAsync(id,userId);

                if (!result)
                {
                    return NotFound(new { success = false, message = "Join request not found." });
                }

                return Ok(new { success = true, message = "Join request deleted successfully." });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
    }
}
