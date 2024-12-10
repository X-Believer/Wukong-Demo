using Microsoft.AspNetCore.Mvc;
using System.Net;
using WukongDemo.Data;
using WukongDemo.inAppMessage.Models;
using WukongDemo.joinRequest.Models;
using WukongDemo.joinRequest.Service;
using WukongDemo.Util;
using WukongDemo.Util.Responses;

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
        public async Task<IActionResult> GetJoinRequestsByProjectId([FromHeader] string authorization, [FromRoute] int projectId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            var joinRequests = await _joinRequestService.GetJoinRequestsByProjectIdAsync(projectId, pageNumber, pageSize);

            if (joinRequests.Item1 == null || !joinRequests.Item1.Any())
            {
                return NotFound(new { errorCode = 404, success = false, message = "Join request not found in the project." });
            }

            // 计算总页数
            var totalPages = (int)Math.Ceiling((double)joinRequests.TotalCount / pageSize);

            var response = new PaginatedResponse<JoinRequest>
            {
                TotalCount = joinRequests.TotalCount,
                TotalPages = joinRequests.TotalCount / pageSize,
                CurrentPage = pageNumber,
                Data = joinRequests.Item1
            };            

            return Ok(response);
        }

        /// <summary>
        /// 获取项目的某一加入申请
        /// </summary>        
        [HttpGet("projects/{projectId}/join-requests/{id}")]
        public async Task<IActionResult> GetJoinRequestById([FromRoute] int id, [FromRoute] int projectId)
        {
            try
            {
                var joinRequest = await _joinRequestService.GetJoinRequestByIdAsync(id, projectId);
                return Ok(joinRequest);
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
                    message = "申请发送成功",
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
        [HttpPut("join-requests/{id}")]
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
        [HttpPut("projects/{projectId}/review-requests/{requestId}")]
        public async Task<IActionResult> ApproveJoinRequestAsync([FromRoute] int requestId, [FromRoute] int projectId, [FromHeader] string authorization, [FromQuery] bool isApproved)
        {
            var userId = AuthUtils.GetUserIdFromToken(authorization);

            try
            {
                var result = await _joinRequestService.ApproveJoinRequestAsync(projectId, requestId, userId, isApproved);
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
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { errorCode = 400, success = false, message = ex.Message });
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
