using Notification.DTOs;

namespace Notification.Services
{
    public interface INotificationService
    {


            Task NotifyTaskCreated(TaskCreatedNotification notification);
            Task NotifyTaskUpdated(TaskUpdatedNotification notification);
            Task NotifyTaskDeleted(TaskDeletedNotification notification);
        
    }
}
