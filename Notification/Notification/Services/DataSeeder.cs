using Microsoft.EntityFrameworkCore;
using Notification.Data;
using Notification.Entities;

namespace Notification.Services
{
    public interface IDataSeeder
    {
        Task SeedAsync();
    }

    public class DataSeeder : IDataSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DataSeeder> _logger;

        public DataSeeder(ApplicationDbContext context, ILogger<DataSeeder> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            try
            {
                _logger.LogInformation("Starting database seeding...");

                await SeedTasksAsync();

                _logger.LogInformation("Database seeding completed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        private async Task SeedTasksAsync()
        {
            if (await _context.Tasks.AnyAsync())
            {
                _logger.LogInformation("Tasks already exist in database. Skipping task seeding.");
                return;
            }

            var tasks = new List<TaskItem>
            {
                new TaskItem
                {
                    Id = Guid.NewGuid(),
                    Title = "Review Project Requirements",
                    Description = "Analyze and review all project requirements and specifications",
                    CreatedAt = DateTime.UtcNow.AddDays(-10),
                    CreatedBy = "project.manager@example.com",
                    UpdatedAt = DateTime.UtcNow.AddDays(-5),
                    UpdatedBy = "business.analyst@example.com",
                    IsDeleted = false
                },
                new TaskItem
                {
                    Id = Guid.NewGuid(),
                    Title = "Design Database Schema",
                    Description = "Create and optimize database schema with proper relationships and indexes",
                    CreatedAt = DateTime.UtcNow.AddDays(-8),
                    CreatedBy = "architect@example.com",
                    IsDeleted = false
                },
                new TaskItem
                {
                    Id = Guid.NewGuid(),
                    Title = "Implement Authentication",
                    Description = "Setup JWT authentication and authorization for the API",
                    CreatedAt = DateTime.UtcNow.AddDays(-6),
                    CreatedBy = "security.team@example.com",
                    IsDeleted = false
                },
                new TaskItem
                {
                    Id = Guid.NewGuid(),
                    Title = "Create Frontend Components",
                    Description = "Develop Angular components for task management interface",
                    CreatedAt = DateTime.UtcNow.AddDays(-4),
                    CreatedBy = "frontend.dev@example.com",
                    IsDeleted = false
                },
                new TaskItem
                {
                    Id = Guid.NewGuid(),
                    Title = "Write Integration Tests",
                    Description = "Create comprehensive integration tests for all API endpoints",
                    CreatedAt = DateTime.UtcNow.AddDays(-2),
                    CreatedBy = "qa.engineer@example.com",
                    IsDeleted = false
                },
                new TaskItem
                {
                    Id = Guid.NewGuid(),
                    Title = "Prepare Deployment Scripts",
                    Description = "Create deployment scripts for development and production environments",
                    CreatedAt = DateTime.UtcNow.AddHours(-12),
                    CreatedBy = "devops@example.com",
                    IsDeleted = false
                },
                new TaskItem
                {
                    Id = Guid.NewGuid(),
                    Title = "Monitor Application Performance",
                    Description = "Setup monitoring and logging for application performance tracking",
                    CreatedAt = DateTime.UtcNow.AddHours(-6),
                    CreatedBy = "monitoring.team@example.com",
                    IsDeleted = false
                },
                new TaskItem
                {
                    Id = Guid.NewGuid(),
                    Title = "Client Training Session",
                    Description = "Prepare and conduct training session for end users",
                    CreatedAt = DateTime.UtcNow.AddHours(-3),
                    CreatedBy = "training.team@example.com",
                    IsDeleted = false
                },
                new TaskItem
                {
                    Id = Guid.NewGuid(),
                    Title = "Gather User Feedback",
                    Description = "Collect and analyze user feedback for future improvements",
                    CreatedAt = DateTime.UtcNow.AddHours(-1),
                    CreatedBy = "product.owner@example.com",
                    IsDeleted = false
                }
            };

            await _context.Tasks.AddRangeAsync(tasks);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Seeded {TaskCount} tasks successfully.", tasks.Count);
        }
    }
}