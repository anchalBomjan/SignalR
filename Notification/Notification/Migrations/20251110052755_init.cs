using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Notification.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "IsDeleted", "Title", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), new DateTime(2025, 11, 5, 5, 27, 55, 93, DateTimeKind.Utc).AddTicks(4386), "system.admin@example.com", "Finish the API documentation for the notification system with detailed examples", false, "Complete Project Documentation", new DateTime(2025, 11, 8, 5, 27, 55, 93, DateTimeKind.Utc).AddTicks(4392), "documentation.team@example.com" },
                    { new Guid("a7b8c9d0-e1f2-3456-7890-123456789012"), new DateTime(2025, 11, 10, 4, 27, 55, 93, DateTimeKind.Utc).AddTicks(4411), "performance.team@example.com", "Optimize database queries and API response times", false, "Performance Optimization", null, null },
                    { new Guid("b2c3d4e5-f6a7-8901-bcde-f23456789012"), new DateTime(2025, 11, 7, 5, 27, 55, 93, DateTimeKind.Utc).AddTicks(4399), "developer@example.com", "Create real-time notification hub for client connections with authentication", false, "Implement SignalR Hub", new DateTime(2025, 11, 9, 5, 27, 55, 93, DateTimeKind.Utc).AddTicks(4400), "senior.developer@example.com" },
                    { new Guid("c3d4e5f6-a7b8-9012-cdef-345678901234"), new DateTime(2025, 11, 9, 5, 27, 55, 93, DateTimeKind.Utc).AddTicks(4402), "tester@example.com", "Write comprehensive unit tests for the notification service layer including edge cases", false, "Test Notification Service", null, null },
                    { new Guid("d4e5f6a7-b8c9-0123-def4-456789012345"), new DateTime(2025, 11, 9, 17, 27, 55, 93, DateTimeKind.Utc).AddTicks(4404), "dba@example.com", "Configure EF Core migrations and database seeding with proper relationships", false, "Setup Database Migrations", null, null },
                    { new Guid("e5f6a7b8-c9d0-1234-ef56-789012345678"), new DateTime(2025, 11, 9, 23, 27, 55, 93, DateTimeKind.Utc).AddTicks(4405), "devops@example.com", "Deploy the application to development environment for testing and validation", false, "Deploy to Development Server", null, null },
                    { new Guid("f6a7b8c9-d0e1-2345-f678-901234567890"), new DateTime(2025, 11, 10, 2, 27, 55, 93, DateTimeKind.Utc).AddTicks(4409), "tech.lead@example.com", "Conduct peer code review for the latest feature implementations", false, "Code Review Session", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CreatedAt",
                table: "Tasks",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CreatedBy",
                table: "Tasks",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_IsDeleted",
                table: "Tasks",
                column: "IsDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
