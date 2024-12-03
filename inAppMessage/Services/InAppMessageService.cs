using WukongDemo.inAppMessage.Models;
using Microsoft.EntityFrameworkCore;
using WukongDemo.Data;
using WukongDemo.user.Models;
using Microsoft.AspNetCore.Identity;

namespace WukongDemo.inAppMessage.Services
{
    public class InAppMessageService
    {
        private readonly AppDbContext _context;

        public InAppMessageService(AppDbContext context)
        {
            _context = context;
        }
        
        // 获取用户站内信
        public async Task<IEnumerable<InAppMessage>> GetMessagesByRecipientAsync(int userId, int pageNumber, int pageSize)
        {
            var query = _context.InAppMessages
                .Where(m => m.RecipientId == userId)
                .OrderByDescending(m => m.SentAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return await query.ToListAsync();
        }

        // 根据id查询站内信
        public async Task<InAppMessage> GetMessageByIdAsync(int id)
        {
            return await _context.InAppMessages.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        }

        // 发送站内信
        public async Task<string> SendMessageAsync(int senderId, int recipientId, int type, string subject, string content, int relatedProjectId = 0)
        {
            // 创建新的站内信对象
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

            // 保存站内信到数据库
            _context.InAppMessages.Add(message);
            await _context.SaveChangesAsync();

            return "Message sent successfully.";
        }

        // 向项目成员发送站内信
        public async Task<string> SendMessageToAllMembers(int projectId, int senderId, int type, string subject, string content)
        {
            // 查询该项目的所有成员
            var projectMembers = await _context.ProjectMembers
                .Where(pm => pm.ProjectId == projectId)
                .Include(pm => pm.User)
                .ToListAsync();

            if (!projectMembers.Any())
            {
                return "No members found for this project.";
            }

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

                // 保存站内信到数据库
                _context.InAppMessages.Add(message);
            }

            // 保存所有新增的站内信
            await _context.SaveChangesAsync();

            return "Messages sent successfully.";
        }

        // 按照id删除站内信
        public async Task<string> DeleteMessageAsync(int id, int userId)
        {
            var message = await _context.InAppMessages.FindAsync(id);

            if (message == null)
            {
                return "NotFound";
            }

            // 检查当前用户是否有权限删除
            if (message.RecipientId != userId)
            {
                return "Forbidden";
            }

            _context.InAppMessages.Remove(message);
            await _context.SaveChangesAsync();

            return "Success";
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
