using Microsoft.EntityFrameworkCore;
using WukongDemo.inAppMessage.Models;
using WukongDemo.joinRequest.Models;
using WukongDemo.project.Models;
using WukongDemo.user.Models;


namespace WukongDemo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<InAppMessage> InAppMessages { get; set; }
        public DbSet<ProjectMember> ProjectMembers { get; set; }
        public DbSet<JoinRequest> JoinRequests { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }

        public const string DbPath = @"WukongDemo.db";

        // 配置 SQLite 数据库
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            optionsBuilder.UseSqlite($"Data Source={DbPath}");           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Project 表
            modelBuilder.Entity<Project>()
                .HasKey(p => p.ProjectId);

            modelBuilder.Entity<Project>()
            .HasOne(p => p.Leader)
            .WithMany()
            .HasForeignKey(p => p.LeaderId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.Instructor)
                .WithMany()
                .HasForeignKey(p => p.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.ProjectMembers)
                .WithOne(pm => pm.Project)
                .HasForeignKey(pm => pm.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Project>()
            .HasMany(p => p.JoinRequests)
            .WithOne(jr => jr.Project)
            .HasForeignKey(jr => jr.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

            // User 表
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<User>()
            .HasMany(u => u.SentMessages)
            .WithOne(im => im.Sender)
            .HasForeignKey(im => im.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.ReceivedMessages)
                .WithOne(im => im.Recipient)
                .HasForeignKey(im => im.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.ProjectMembers)
                .WithOne(pm => pm.User)
                .HasForeignKey(pm => pm.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.JoinRequestsAsApplicant)
                .WithOne(jr => jr.Applicant)
                .HasForeignKey(jr => jr.ApplicantId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.JoinRequestsAsReviewer)
                .WithOne(jr => jr.Reviewer)
                .HasForeignKey(jr => jr.ReviewedBy)
                .OnDelete(DeleteBehavior.SetNull);

            // InAppMessage 表
            modelBuilder.Entity<InAppMessage>()
                .HasKey(pm => pm.InAppMessageId);

          
            modelBuilder.Entity<InAppMessage>()
                .HasOne(m => m.RelatedProject)
                .WithMany()
                .HasForeignKey(m => m.RelatedProjectId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<InAppMessage>()
                .Property(im => im.Type)
                .HasConversion<int>();

            // ProjectMember 表
            modelBuilder.Entity<ProjectMember>()
                .HasKey(pm => pm.ProjectMemberId);

            modelBuilder.Entity<ProjectMember>()
            .HasOne(pm => pm.Project)
            .WithMany(p => p.ProjectMembers)
            .HasForeignKey(pm => pm.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProjectMember>()
                .HasOne(pm => pm.User)
                .WithMany(u => u.ProjectMembers)
                .HasForeignKey(pm => pm.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //  JoinRequest 表
            modelBuilder.Entity<JoinRequest>()
            .HasKey(jr => jr.JoinRequestId);

            modelBuilder.Entity<JoinRequest>()
                .HasOne(jr => jr.Project)
                .WithMany(p => p.JoinRequests)
                .HasForeignKey(jr => jr.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<JoinRequest>()
                .HasOne(jr => jr.Applicant)
                .WithMany(u => u.JoinRequestsAsApplicant)
                .HasForeignKey(jr => jr.ApplicantId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JoinRequest>()
                .HasOne(jr => jr.Reviewer)
                .WithMany(u => u.JoinRequestsAsReviewer)
                .HasForeignKey(jr => jr.ReviewedBy)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
