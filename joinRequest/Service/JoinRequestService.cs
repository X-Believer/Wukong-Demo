using Azure.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using WukongDemo.Data;
using WukongDemo.joinRequest.Models;
using WukongDemo.project.Service;

namespace WukongDemo.joinRequest.Service
{
    public class JoinRequestService
    {
        private readonly AppDbContext _context;
        private readonly ProjectMemberService _projectMemberService;

        public JoinRequestService(AppDbContext context, ProjectMemberService projectMemberService)
        {
            _projectMemberService = projectMemberService;
            _context = context;
        }

        // 获取某一项目的全部加入申请
        public async Task<(IEnumerable<JoinRequest>, int TotalCount)> GetJoinRequestsByProjectIdAsync(int projectId, int pageNumber, int pageSize)
        {
            var totalCount = await _context.JoinRequests
                .Where(jr => jr.ProjectId == projectId)
                .CountAsync();

            var joinRequests = await _context.JoinRequests
                .Where(jr => jr.ProjectId == projectId)
                .Include(jr => jr.Applicant)
                //.Include(jr => jr.Reviewer)
                .OrderBy(jr => jr.SubmittedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (joinRequests, totalCount);
        }

        // 根据项目申请ID查找项目申请
        public async Task<JoinRequest> GetJoinRequestByIdAsync(int id, int projectId)
        {
#pragma warning disable CS8603
            var request = await _context.JoinRequests
                .Include(jr => jr.Applicant)
                .Include(jr => jr.Reviewer)
                .FirstOrDefaultAsync(jr => jr.JoinRequestId == id);
#pragma warning restore CS8603
            if(request==null||request.ProjectId!=projectId)
            {
                throw new KeyNotFoundException("Join request not found.");
            }
            return request;
        }

        // 发送加入申请
        public async Task<JoinRequest> SendJoinRequestAsync(int projectId, int applicantId, string type, string reason, string selfIntroduction)
        {
            if (await _context.Projects.FindAsync(projectId) == null)
            {
                throw new KeyNotFoundException("Project not found.");
            }

            var joinRequest = new JoinRequest
            {
                ProjectId = projectId,
                ApplicantId = applicantId,
                Type = type,
                Reason = reason,
                SelfIntroduction = selfIntroduction,
                Status = "Pending",
                SubmittedAt = DateTime.UtcNow
            };

            // 保存到数据库
            _context.JoinRequests.Add(joinRequest);
            await _context.SaveChangesAsync();

            return joinRequest;
        }

        // 修改加入申请
        public async Task<JoinRequest> UpdateJoinRequestAsync(int requestId, int userId, string type, string reason, string selfIntroduction)
        {
            var joinRequest = await _context.JoinRequests.FindAsync(requestId);

            if (joinRequest == null)
            {
                throw new KeyNotFoundException("Join request not found.");
            }

            if (joinRequest.ApplicantId != userId)
            {
                throw new UnauthorizedAccessException("Access denied.");
            }

            joinRequest.Type = type;
            joinRequest.Reason = reason;
            joinRequest.SelfIntroduction = selfIntroduction;

            _context.JoinRequests.Update(joinRequest);
            await _context.SaveChangesAsync();

            return joinRequest;
        }

        // 审核加入申请
        public async Task<string> ApproveJoinRequestAsync(int projectId, int requestId, int userId, bool isApproved)
        {
            try
            {
                JoinRequest joinRequest = await GetJoinRequestByIdAsync(requestId, projectId);
                var isAuthorized = await IsUserProjectLeaderOrAdminAsync(joinRequest.ProjectId, userId);
                if (!isAuthorized)
                {
                    throw new UnauthorizedAccessException("Access denied.");
                }

                if (isApproved)
                {
                    if (joinRequest.Project.CurrentMembers >= joinRequest.Project.MaxMembers)
                    {
                        throw new InvalidOperationException("Max member reached.");
                    }

                    try
                    {
                        var result = await _projectMemberService.AddProjectMemberAsync(joinRequest.ProjectId, userId, joinRequest.ApplicantId, joinRequest.Type);
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException(ex.Message);
                    }

                    joinRequest.Status = "Approved";
                    joinRequest.ReviewedBy = userId;
                    joinRequest.ReviewedAt = DateTime.Now;

                    await _context.SaveChangesAsync();

                    return "Join request is approved.";
                }
                else
                {
                    joinRequest.Status = "Rejected";
                    joinRequest.ReviewedBy = userId;
                    joinRequest.ReviewedAt = DateTime.Now;

                    await _context.SaveChangesAsync();

                    return "Join request is rejected.";
                }
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }    
        }

        // 删除加入申请
        public async Task<string> DeleteJoinRequestAsync(int requestId, int userId)
        {
            var joinRequest = await _context.JoinRequests.FindAsync(requestId);

            if (joinRequest == null)
            {
                throw new KeyNotFoundException("Join request not found.");
            }

            if (joinRequest.ApplicantId != userId)
            {
                throw new UnauthorizedAccessException("Access denied.");
            }

            _context.JoinRequests.Remove(joinRequest);
            await _context.SaveChangesAsync();

            return "Join request removed successfully.";
        }

        // 检查请求者是否是项目的负责人或管理员
        private async Task<bool> IsUserProjectLeaderOrAdminAsync(int projectId, int userId)
        {
            var project = await _context.Projects
                .Include(p => p.Leader)
                .FirstOrDefaultAsync(p => p.ProjectId == projectId);

            if (project == null)
            {
                return false;
            }

            return project.LeaderId == userId;
        }
    }
}
