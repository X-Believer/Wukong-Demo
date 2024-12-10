using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WukongDemo.Data;
using WukongDemo.inAppMessage.Models;
using WukongDemo.project.Models;
using WukongDemo.Util;

namespace WukongDemo.project.Service
{
    public class ProjectMemberService
    {
        private readonly AppDbContext _context;
        private readonly ProjectService _projectService;

        public ProjectMemberService(AppDbContext context, ProjectService projectService)
        {
            _projectService=projectService;
            _context = context;
        }

        // 获取项目全部成员
        public async Task<IEnumerable<ProjectMember>> GetProjectMembersAsync(int projectId)
        {
            var projectMembers = await _context.ProjectMembers
                .Where(pm => pm.ProjectId == projectId)
                .Include(pm => pm.User)
                .ToListAsync();

            return projectMembers;
        }

        // 新增项目成员
        public async Task<ProjectMember> AddProjectMemberAsync(int projectId, int userId, int newMemberId, [ValidRole] string role)
        {
            var isAuthorized =  await IsUserProjectLeaderOrAdminAsync(projectId, userId);
            if (!isAuthorized)
            {
                throw new UnauthorizedAccessException("Access denied.");
            }
            if (await _context.Users.FindAsync(newMemberId) == null)
            {
                throw new KeyNotFoundException("User not found.");
            }
            Project project = await _context.Projects.FindAsync(projectId);
            if (project == null)
            {
                throw new KeyNotFoundException("Project not found.");
            }
            if (project.CurrentMembers >= project.MaxMembers)
            {
                throw new InvalidOperationException("Reached max member.");
            }
            if (await IsUserInProjectAsync(projectId, newMemberId))
            {
                throw new InvalidOperationException("Already a Member.");
            }

            var newMember = new ProjectMember
            {
                ProjectId = projectId,
                UserId = newMemberId,
                Role = role,
                JoinDate = DateTime.Now,
                Status = "Active"
            };

            _context.ProjectMembers.Add(newMember);
            await _context.SaveChangesAsync();

            return newMember;
        }

        // 变更项目成员身份
        public async Task<string> ChangeMemberRoleAsync(int projectId, int userId, [ValidRole] string newRole, int updateId)
        {
            var isAuthorized = await IsUserProjectLeaderOrAdminAsync(projectId, userId);
            
            if (!isAuthorized)
            {
                throw new UnauthorizedAccessException("Access denied.");
            }

            var projectMember = await _context.ProjectMembers
                .FirstOrDefaultAsync(pm => pm.ProjectId == projectId && pm.UserId == updateId && pm.Status == "Active");

            if (projectMember == null)
            {
                throw new KeyNotFoundException("Member not found in the project.");
            }

            if (newRole == "ProjectLeader")
            {
                var currentLeader = await _context.ProjectMembers
                    .FirstOrDefaultAsync(pm => pm.ProjectId == projectId && pm.Role == "ProjectLeader" && pm.Status == "Active");

                if (currentLeader != null) currentLeader.Role = "Member";
                projectMember.Role = "ProjectLeader";
            }

            else projectMember.Role = newRole;

            await _context.SaveChangesAsync();

            return $"Member role updated to {projectMember.Role} successfully.";
        }

        // 删除项目成员
        public async Task<string> RemoveProjectMemberAsync(int projectId, int userId, int deleteId)
        {
            var isAuthorized = await IsUserProjectLeaderOrAdminAsync(projectId, userId);
            if (!isAuthorized||userId==deleteId)// 不能删除自己（负责人）
            {
                throw new UnauthorizedAccessException("You do not have permission to remove this member.");
            }

            var projectMember = await _context.ProjectMembers
                .FirstOrDefaultAsync(pm => pm.ProjectId == projectId && pm.UserId == deleteId && pm.Status == "Active");

            if (projectMember == null)
            {
                throw new KeyNotFoundException("Member not found in the project.");
            }

            _context.ProjectMembers.Remove(projectMember);
            await _context.SaveChangesAsync();

            return "Member removed successfully.";
        }


        // 检查用户是否属于某个项目
        private async Task<bool> IsUserInProjectAsync(int projectId, int userId)
        {
            var isMember = await _context.ProjectMembers
                .AnyAsync(pm => pm.ProjectId == projectId && pm.UserId == userId && pm.Status == "Active");

            return isMember;
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
