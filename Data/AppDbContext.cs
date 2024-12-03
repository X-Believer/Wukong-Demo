using Microsoft.EntityFrameworkCore;
using WukongDemo.inAppMessage.Models;
using WukongDemo.project.Models;
using WukongDemo.recruitment.Models;
using WukongDemo.user.Models;


namespace WukongDemo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<InAppMessage> InAppMessages { get; set; }
        public DbSet<ProjectMember> ProjectMembers { get; set; }

        public const string DbPath = @"WukongDemo.db";

        // 配置 SQLite 数据库
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"Data Source={DbPath}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 配置 Project 表
            modelBuilder.Entity<Project>()
                .HasKey(p => p.Id);  // 主键配置

            modelBuilder.Entity<Project>()
                .HasOne(p => p.Instructor)
                .WithMany()
                .HasForeignKey(p => p.InstructorId);  // 外键配置

            // 配置 ProjectMember 表
            modelBuilder.Entity<ProjectMember>()
                .HasKey(pm => pm.ProjectMemberId);  // 主键配置

            modelBuilder.Entity<ProjectMember>()
                .HasOne(pm => pm.Project)
                .WithMany(p => p.ProjectMembers)  // 一个项目有多个成员
                .HasForeignKey(pm => pm.ProjectId)  // 外键配置
                .OnDelete(DeleteBehavior.Cascade);  // 删除项目时级联删除成员

            modelBuilder.Entity<ProjectMember>()
                .HasOne(pm => pm.User)
                .WithMany()  // 一个用户可以加入多个项目
                .HasForeignKey(pm => pm.UserId);  // 外键配置

            // 配置 ProjectMember 的属性
            modelBuilder.Entity<ProjectMember>()
                .Property(pm => pm.Role)
                .HasMaxLength(50);

            modelBuilder.Entity<ProjectMember>()
                .Property(pm => pm.Status)
                .HasMaxLength(20);

            modelBuilder.Entity<ProjectMember>()
                .Property(pm => pm.JoinDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");  // 加入日期默认值为当前时间

            // 配置 User 表的属性
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);  // 假设 User 类有一个 Id 属性作为主键

            modelBuilder.Entity<InAppMessage>()
                .Property(im => im.Type)
                .HasConversion<int>();  // 将枚举类型 MessageType 存储为整数
        }
    }
}
