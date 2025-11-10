using MediatR;
using Notification.Data;
using Notification.DTOs;
using Notification.Services;

namespace Notification.Commands
{
    public class DeleteTaskCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string DeletedBy { get; set; } = string.Empty;
    }

    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly INotificationService _notificationService;

        public DeleteTaskCommandHandler(IApplicationDbContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _context.Tasks.FindAsync(new object[] { request.Id }, cancellationToken);

            if (task == null) throw new Exception($"Task with ID {request.Id} not found.");

            task.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);

            await _notificationService.NotifyTaskDeleted(new TaskDeletedNotification
            {
                TaskId = task.Id,
                Title = task.Title,
                DeletedAt = DateTime.UtcNow,
                DeletedBy = request.DeletedBy
            });

            return Unit.Value;
        }
    }


}
