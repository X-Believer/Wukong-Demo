using Microsoft.EntityFrameworkCore;
using WukongDemo.Data;
using WukongDemo.user.Models;
using WukongDemo.project.Models;

namespace WukongDemo.project.Service
{
    public class ProjectService
    {
        private readonly AppDbContext _context;

        public ProjectService(AppDbContext context)
        {
            _context = context;
        }

        // 根据项目ID查找项目
        public async Task<Project> GetProjectByIdAsync(int projectId)
        {
#pragma warning disable CS8603
            return await _context.Projects
                .Include(p => p.Leader)
                .Include(p => p.Instructor)
                .FirstOrDefaultAsync(p => p.ProjectId == projectId);
#pragma warning restore CS8603
        }

        // 获取某一项目的负责人
        public async Task<User> GetProjectLeaderAsync(int projectId)
        {
            var project = await _context.Projects
                .Include(p => p.Leader)  // 加载项目的负责人信息
                .FirstOrDefaultAsync(p => p.ProjectId == projectId);

            if (project == null)
            {
                return null;
            }

            return project.Leader;  // 返回项目的负责人
        }
    }
}
