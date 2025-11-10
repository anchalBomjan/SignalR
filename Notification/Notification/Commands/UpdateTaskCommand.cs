using MediatR;
using Notification.Data;
using Notification.DTOs;
using Notification.Services;

namespace Notification.Commands
{


    public class UpdateTaskCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
    }

    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly INotificationService _notificationService;

        public UpdateTaskCommandHandler(IApplicationDbContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        public async Task<Unit> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _context.Tasks.FindAsync(new object[] { request.Id }, cancellationToken);

            if (task == null) throw new Exception($"Task with ID {request.Id} not found.");

            task.Title = request.Title;
            task.Description = request.Description;
            task.UpdatedAt = DateTime.UtcNow;
            task.UpdatedBy = request.UpdatedBy;

            await _context.SaveChangesAsync(cancellationToken);

            await _notificationService.NotifyTaskUpdated(new TaskUpdatedNotification
            {
                TaskId = task.Id,
                Title = task.Title,
                Description = task.Description,
                UpdatedAt = task.UpdatedAt.Value,
                UpdatedBy = task.UpdatedBy
            });

            return Unit.Value;
        }
    }
}
