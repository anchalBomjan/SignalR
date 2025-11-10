using Microsoft.AspNetCore.SignalR;
using Notification.DTOs;
using Notification.Hubs;

namespace Notification.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyTaskCreated(TaskCreatedNotification notification)
        {
            await _hubContext.Clients.Group("tasks")
                .SendAsync("TaskCreated", notification);
        }

        public async Task NotifyTaskUpdated(TaskUpdatedNotification notification)
        {
            await _hubContext.Clients.Group("tasks")
                .SendAsync("TaskUpdated", notification);
        }

        public async Task NotifyTaskDeleted(TaskDeletedNotification notification)
        {
            await _hubContext.Clients.Group("tasks")
                .SendAsync("TaskDeleted", notification);
        }
    }
}
