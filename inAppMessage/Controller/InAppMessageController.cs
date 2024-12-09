using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WukongDemo.Data;
using WukongDemo.inAppMessage.Models;
using WukongDemo.Util;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using WukongDemo.inAppMessage.Service;

namespace WukongDemo.inAppMessage.Controller
{
    [Route("")]
    [ApiController]
    public class InAppMessageController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly InAppMessageService _inAppMessageService;

        public InAppMessageController(AppDbContext context, InAppMessageService inAppMessageService)
        {
            _context = context;
            _inAppMessageService = inAppMessageService;
        }

        /// <summary>
        /// 获取用户所有站内信
        /// </summary>
        [HttpGet("messages")]
        public async Task<IActionResult> GetMessages([FromHeader] string authorization, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var userId = AuthUtils.GetUserIdFromToken(authorization);

            // 从 Service 层获取站内信列表
            var messages = await _inAppMessageService.GetMessagesByRecipientAsync(userId, pageNumber, pageSize);

            if (messages == null || !messages.Any())
            {
                return NotFound(new { errorCode = 404, success = false, message = "No message found for the user." });
            }

            return Ok(messages);
        }

        /// <summary>
        /// 查询站内信详细
        /// </summary>      
        [HttpGet("messages/{id}")]
        public async Task<IActionResult> GetMessageById([FromHeader] string authorization, [FromRoute] int id)
        {
            var userId = AuthUtils.GetUserIdFromToken(authorization);

            try
            {
                var message = await _inAppMessageService.GetMessageByIdAsync(id, userId);
                return Ok(message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { errorCode = 404, success = false, message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { errorCode = 401, success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }

        }

        /// <summary>
        /// 向指定用户发送站内信
        /// </summary>
        [HttpPost("message")]
        public async Task<IActionResult> SendMessage([FromHeader] string authorization, [FromBody] SendMessageRequest request)
        {
            var senderId = AuthUtils.GetUserIdFromToken(authorization);

            try
            {
                var newMessage = await _inAppMessageService.SendMessageAsync(senderId, request.RecipientId, request.Type, request.Subject, request.Content, request.RelatedProjectId);

                return Ok(new
                {
                    success = true,
                    message = "站内信发送成功",
                    data = newMessage
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
        /// 向项目所有成员发送站内信
        /// </summary>
        [HttpPost("projects/{projectId}/messages")]
        public async Task<IActionResult> SendMessageToAllMembers([FromRoute] int projectId, [FromHeader] string authorization, [FromBody] SendMessageRequest request)
        {
            var senderId = AuthUtils.GetUserIdFromToken(authorization);

            try
            {
                var newMessages = await _inAppMessageService.SendMessageToAllMembers(projectId, senderId, request.Type, request.Subject, request.Content);
                
                return Ok(new
                {
                    success = true,
                    message = "站内信发送成功",
                    data = newMessages
                });
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { errorCode = 404, success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// 按照id删除站内信
        /// </summary>
        [HttpDelete("message/{id}")]
        public async Task<IActionResult> DeleteMessageById([FromHeader] string authorization, [FromRoute] int id)
        {
            var userId = AuthUtils.GetUserIdFromToken(authorization);

            try
            {
                var result = await _inAppMessageService.DeleteMessageAsync(id, userId);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { errorCode = 404, success = false, message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { errorCode = 401, success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }

            return Ok(new { success = true, message = "站内信删除成功" });
        }

     
    }
}
