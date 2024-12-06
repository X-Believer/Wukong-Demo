using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WukongDemo.Data;
using WukongDemo.inAppMessage.Models;
using WukongDemo.Util;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using WukongDemo.inAppMessage.Service;

namespace WukongDemo.inAppMessage.Controllers
{
    [Route("api/[controller]")]
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
                return NotFound(new { errorCode = "404", message = "No messages found." });
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

            // 从数据库中查询站内信
            var message = await _inAppMessageService.GetMessageByIdAsync(id);
            if (message == null)
            {
                return NotFound(new { errorCode = "404", message = "Message not found." });
            }

            // 确保当前用户是收件人或发件人
            if (message.RecipientId != userId && message.SenderId != userId)
            {
                return Forbid("Access denied.");
            }

            return Ok(message);
        }

        /// <summary>
        /// 向指定用户发送站内信
        /// </summary>
        [HttpPost("message")]
        public async Task<IActionResult> SendMessage([FromHeader] string authorization, [FromBody] SendMessageRequest request)
        {
            var senderId = AuthUtils.GetUserIdFromToken(authorization);

            var result = await _inAppMessageService.SendMessageAsync(senderId, request.RecipientId, request.Type, request.Subject, request.Content, request.RelatedProjectId);

            return Ok(new { success = true, message = result });
        }

        /// <summary>
        /// 向项目所有成员发送站内信
        /// </summary>
        [HttpPost("projects/{projectId}/messages")]
        public async Task<IActionResult> SendMessageToAllMembers([FromRoute] int projectId, [FromHeader] string authorization, [FromBody] SendMessageRequest request)
        {
            var senderId = AuthUtils.GetUserIdFromToken(authorization);

            
            var result = await _inAppMessageService.SendMessageToAllMembers(projectId, senderId, request.Type, request.Subject, request.Content);

            if (result.Contains("successfully"))
            {
                return Ok(new { success = true, message = result });
            }

            return BadRequest(new { success = false, message = result });
        }

        /// <summary>
        /// 按照id删除站内信
        /// </summary>
        [HttpDelete("message/{id}")]
        public async Task<IActionResult> DeleteMessageById([FromHeader] string authorization, [FromRoute] int id)
        {
            var userId = AuthUtils.GetUserIdFromToken(authorization);

            var result = await _inAppMessageService.DeleteMessageAsync(id, userId);

            if (result == "NotFound")
            {
                return NotFound(new { errorCode = "404", message = "Message not found." });
            }
            else if (result == "Forbidden")
            {
                return Forbid("Access denied.");
            }

            return Ok(new { success = true, message = "站内信删除成功" });
        }

     
    }
}
