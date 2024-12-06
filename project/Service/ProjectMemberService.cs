using Microsoft.EntityFrameworkCore;
using WukongDemo.Data;
using WukongDemo.project.Models;

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
        public async Task<string> AddProjectMemberAsync(int projectId, int userId, int newMemberId, string role)
        {
            var isAuthorized = await IsUserProjectLeaderOrAdminAsync(projectId, userId);
            if (!isAuthorized)
            {
                return "Forbidden";
            }

            if (await IsUserInProjectAsync(projectId, newMemberId))
            {
                return "AlreadyMember";
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

            return "MemberAdded";
        }

        // 变更项目成员身份
        public async Task<string> ChangeMemberRoleAsync(int projectId, int userId, string newRole, int updateId)
        {
            var isAuthorized = await IsUserProjectLeaderOrAdminAsync(projectId, userId);
            if (!isAuthorized)
            {
                return "Forbidden";
            }

            var projectMember = await _context.ProjectMembers
                .FirstOrDefaultAsync(pm => pm.ProjectId == projectId && pm.UserId == updateId && pm.Status == "Active");

            if (projectMember == null)
            {
                return "NotFound";
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

            return "Success";
        }

        // 删除项目成员
        public async Task<string> RemoveProjectMemberAsync(int projectId, int userId, int deleteId)
        {
            var isAuthorized = await IsUserProjectLeaderOrAdminAsync(projectId, userId);
            if (!isAuthorized||userId==deleteId)// 不能删除自己（负责人）
            {
                return "Forbidden";
            }

            var projectMember = await _context.ProjectMembers
                .FirstOrDefaultAsync(pm => pm.ProjectId == projectId && pm.UserId == deleteId && pm.Status == "Active");

            if (projectMember == null)
            {
                return "NotFound";
            }

            _context.ProjectMembers.Remove(projectMember);
            await _context.SaveChangesAsync();

            return "Success";
        }


        // 检查用户是否属于某个项目
        public async Task<bool> IsUserInProjectAsync(int projectId, int userId)
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
