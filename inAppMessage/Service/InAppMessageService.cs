using WukongDemo.inAppMessage.Models;
using Microsoft.EntityFrameworkCore;
using WukongDemo.Data;
using WukongDemo.user.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using WukongDemo.Util.Responses;

namespace WukongDemo.inAppMessage.Service
{
    public class InAppMessageService
    {
        private readonly AppDbContext _context;

        public InAppMessageService(AppDbContext context)
        {
            _context = context;
        }
        
        // 获取用户站内信
        public async Task<(IEnumerable<InAppMessage>, int totalCount)> GetMessagesByRecipientAsync(int userId, int pageNumber, int pageSize)
        {
            var totalCount = await _context.InAppMessages
                .Where(ms => ms.RecipientId == userId)
                .CountAsync();

            var query = _context.InAppMessages
                .Where(m => m.RecipientId == userId)
                .OrderByDescending(m => m.SentAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return (await query.ToListAsync(), totalCount);
        }

        // 根据id查询站内信
        public async Task<InAppMessage> GetMessageByIdAsync(int id, int userId)
        {
            InAppMessage message = await _context.InAppMessages.FindAsync(id);
            if (message == null)
            {
                throw new KeyNotFoundException("Message not found.");
            }
            if (message.RecipientId != userId && message.SenderId != userId)
            {
                throw new UnauthorizedAccessException("Access Denied");
            }
            await MarkMessageAsReadAsync(id);
            return message;
        }

        // 发送站内信
        public async Task<InAppMessage> SendMessageAsync(int senderId, int recipientId, int type, string subject, string content, int relatedProjectId = 0)
        {
            if (await _context.Users.FindAsync(recipientId) == null)
            {
                throw new KeyNotFoundException("Recipient not found.");
            }

            var message = new InAppMessage
            {
                SenderId = senderId,
                RecipientId = recipientId,
                Type = type,
                RelatedProjectId = relatedProjectId,
                Subject = subject,
                Content = content,
                SentAt = DateTime.Now,
                IsRead = false,
            };

            _context.InAppMessages.Add(message);
            await _context.SaveChangesAsync();

            return message;
        }

        // 向项目成员发送站内信
        public async Task<List<InAppMessage>> SendMessageToAllMembers(int projectId, int senderId, int type, string subject, string content)
        {
            var senderRecords = await _context.ProjectMembers
                .Where(pm => pm.UserId == senderId&& pm.ProjectId == projectId)
                .ToListAsync();
            if (senderRecords==null)
            {
                throw new KeyNotFoundException("Sender is not a member in any project.");
            }
            
            var projectMembers = await _context.ProjectMembers
                .Where(pm => pm.ProjectId == projectId)
                .Include(pm => pm.User)
                .ToListAsync();

            if (projectMembers.Count == 0)
            {
                throw new KeyNotFoundException("No member found in the project");
            }
            List<InAppMessage> messages = new();

            // 遍历每个成员并发送站内信
            foreach (var projectMember in projectMembers)
            {
                var message = new InAppMessage
                {
                    SenderId = senderId,
                    RecipientId = projectMember.UserId,
                    Type = type,
                    Subject = subject,
                    Content = content,
                    SentAt = DateTime.Now,
                    IsRead = false,
                    RelatedProjectId = projectId
                };
                messages.Add(message);

                _context.InAppMessages.Add(message);
            }

            await _context.SaveChangesAsync();

            return messages;
        }

        // 按照id删除站内信
        public async Task<string> DeleteMessageAsync(int id, int userId)
        {
            var message = await _context.InAppMessages.FindAsync(id);

            if (message == null)
            {
                throw new KeyNotFoundException("Messge not found.");
            }

            if (message.RecipientId != userId)
            {
                throw new UnauthorizedAccessException("Access denied.");
            }

            _context.InAppMessages.Remove(message);
            await _context.SaveChangesAsync();

            return "Message deleted successfully.";
        }

        // 标记为已读
        public async Task<bool> MarkMessageAsReadAsync(int messageId)
        {
            var message = await _context.InAppMessages.FindAsync(messageId);
            if (message == null)
            {
                return false;
            }

            message.IsRead = true;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
