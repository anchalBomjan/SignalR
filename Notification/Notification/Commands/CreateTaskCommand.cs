using MediatR;
using Notification.Data;
using Notification.DTOs;
using Notification.Entities;
using Notification.Services;

namespace Notification.Commands
{
    public class CreateTaskCommand : IRequest<Guid>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
    }

    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly INotificationService _notificationService;

        public CreateTaskCommandHandler(IApplicationDbContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = request.CreatedBy,
                IsDeleted = false
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync(cancellationToken);

            await _notificationService.NotifyTaskCreated(new TaskCreatedNotification
            {
                TaskId = task.Id,
                Title = task.Title,
                Description = task.Description,
                CreatedAt = task.CreatedAt,
                CreatedBy = task.CreatedBy
            });

            return task.Id;
        }
    }

}
