using Microsoft.AspNetCore.Mvc;
using System.Net;
using WukongDemo.Data;
using WukongDemo.joinRequest.Models;
using WukongDemo.joinRequest.Service;
using WukongDemo.Util;

namespace WukongDemo.joinRequest.Controller
{
    [Route("")]
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
            var (joinRequests, totalCount) = await _joinRequestService.GetJoinRequestsByProjectIdAsync(projectId, pageNumber, pageSize);

            if (joinRequests == null || !joinRequests.Any())
            {
                return NotFound(new { errorCode = 404, success = false, message = "Join request not found in the project." });
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
                return NotFound(new { errorCode = 404, success = false, message = "Join request not found." });
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
                return BadRequest(new { success = false, message = "Invalid request data." });
            }
            var applicantId = AuthUtils.GetUserIdFromToken(authorization);

            try
            {
                var joinRequest = await _joinRequestService.SendJoinRequestAsync(dto.ProjectId, applicantId, dto.Type, dto.Reason, dto.SelfIntroduction);
                return Ok(new
                {
                    success = true,
                    message = "添加成员成功",
                    data = joinRequest
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { errorCode = 404, success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// 修改加入申请
        /// </summary>
        [HttpPost("join-requests/{id}")]
        public async Task<IActionResult> UpdateJoinRequest([FromHeader] string authorization, [FromRoute] int id, [FromBody] JoinRequestDto updateDto)
        {
            var userId = AuthUtils.GetUserIdFromToken(authorization);
            try
            {
                var updatedRequest = await _joinRequestService.UpdateJoinRequestAsync(id, userId, updateDto.Type, updateDto.Reason, updateDto.SelfIntroduction);

                return Ok(new
                {
                    success = true,
                    message = "申请修改成功",
                    data = updatedRequest
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { errorCode = 401, success = false, message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { errorCode = 404, success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// 审核加入申请
        /// </summary>
        [HttpPut("review-join-requests/{requestId}")]
        public async Task<IActionResult> ApproveJoinRequestAsync([FromRoute] int requestId, [FromHeader] string authorization, [FromQuery] bool isApproved)
        {
            var userId = AuthUtils.GetUserIdFromToken(authorization);

            try
            {
                var result = await _joinRequestService.ApproveJoinRequestAsync(requestId, userId, isApproved);
                return Ok(new { message = result });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { errorCode = 401, success = false, message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { errorCode = 404, success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
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
                
                return Ok(new { success = true, message = result });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { errorCode = 401, success = false, message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { errorCode = 404, success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
    }
}
