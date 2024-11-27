using Microsoft.EntityFrameworkCore;
using WukongDemo.Models;

namespace WukongDemo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // 数据库表：Recruitments
        public DbSet<Recruitment> Recruitments { get; set; }

        // 其他实体，假如有的话：
        // public DbSet<Project> Projects { get; set; }

        // 可以在这里配置表的行为（如表名、列名等）
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 对Recruitment表的自定义配置
            modelBuilder.Entity<Recruitment>()
                .HasKey(r => r.Id);  // 主键配置

            modelBuilder.Entity<Recruitment>()
                .Property(r => r.Status)
                .HasDefaultValue("open"); // 默认值配置
        }
    }
}
