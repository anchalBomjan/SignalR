using Microsoft.EntityFrameworkCore;
using Notification.Entities;

namespace Notification.Data
{
    public interface IApplicationDbContext
    {
        DbSet<TaskItem> Tasks { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Description)
                    .HasMaxLength(1000);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedAt)
                    .IsRequired();

                entity.HasQueryFilter(e => !e.IsDeleted);

                entity.HasIndex(e => e.CreatedAt);
                entity.HasIndex(e => e.CreatedBy);
                entity.HasIndex(e => e.IsDeleted);
            });
            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }



        private void SeedData(ModelBuilder modelBuilder)
        {
            var task1Id = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890");
            var task2Id = Guid.Parse("b2c3d4e5-f6a7-8901-bcde-f23456789012");
            var task3Id = Guid.Parse("c3d4e5f6-a7b8-9012-cdef-345678901234");
            var task4Id = Guid.Parse("d4e5f6a7-b8c9-0123-def4-456789012345");
            var task5Id = Guid.Parse("e5f6a7b8-c9d0-1234-ef56-789012345678");

            modelBuilder.Entity<TaskItem>().HasData(
                new TaskItem
                {
                    Id = task1Id,
                    Title = "Complete Project Documentation",
                    Description = "Finish the API documentation for the notification system with detailed examples",
                    CreatedAt = DateTime.UtcNow.AddDays(-5),
                    CreatedBy = "system.admin@example.com",
                    UpdatedAt = DateTime.UtcNow.AddDays(-2),
                    UpdatedBy = "documentation.team@example.com",
                    IsDeleted = false
                },
                new TaskItem
                {
                    Id = task2Id,
                    Title = "Implement SignalR Hub",
                    Description = "Create real-time notification hub for client connections with authentication",
                    CreatedAt = DateTime.UtcNow.AddDays(-3),
                    CreatedBy = "developer@example.com",
                    UpdatedAt = DateTime.UtcNow.AddDays(-1),
                    UpdatedBy = "senior.developer@example.com",
                    IsDeleted = false
                },
                new TaskItem
                {
                    Id = task3Id,
                    Title = "Test Notification Service",
                    Description = "Write comprehensive unit tests for the notification service layer including edge cases",
                    CreatedAt = DateTime.UtcNow.AddDays(-1),
                    CreatedBy = "tester@example.com",
                    IsDeleted = false
                },
                new TaskItem
                {
                    Id = task4Id,
                    Title = "Setup Database Migrations",
                    Description = "Configure EF Core migrations and database seeding with proper relationships",
                    CreatedAt = DateTime.UtcNow.AddHours(-12),
                    CreatedBy = "dba@example.com",
                    IsDeleted = false
                },
                new TaskItem
                {
                    Id = task5Id,
                    Title = "Deploy to Development Server",
                    Description = "Deploy the application to development environment for testing and validation",
                    CreatedAt = DateTime.UtcNow.AddHours(-6),
                    CreatedBy = "devops@example.com",
                    IsDeleted = false
                },
                new TaskItem
                {
                    Id = Guid.Parse("f6a7b8c9-d0e1-2345-f678-901234567890"),
                    Title = "Code Review Session",
                    Description = "Conduct peer code review for the latest feature implementations",
                    CreatedAt = DateTime.UtcNow.AddHours(-3),
                    CreatedBy = "tech.lead@example.com",
                    IsDeleted = false
                },
                new TaskItem
                {
                    Id = Guid.Parse("a7b8c9d0-e1f2-3456-7890-123456789012"),
                    Title = "Performance Optimization",
                    Description = "Optimize database queries and API response times",
                    CreatedAt = DateTime.UtcNow.AddHours(-1),
                    CreatedBy = "performance.team@example.com",
                    IsDeleted = false
                }
            );
        }
    }
}
