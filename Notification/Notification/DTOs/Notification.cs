namespace Notification.DTOs
{

        public class TaskCreatedNotification
        {
            public Guid TaskId { get; set; }
            public string Title { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public DateTime CreatedAt { get; set; }
            public string CreatedBy { get; set; } = string.Empty;
        }

        public class TaskUpdatedNotification
        {
            public Guid TaskId { get; set; }
            public string Title { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public DateTime UpdatedAt { get; set; }
            public string UpdatedBy { get; set; } = string.Empty;
        }

        public class TaskDeletedNotification
        {
            public Guid TaskId { get; set; }
            public string Title { get; set; } = string.Empty;
            public DateTime DeletedAt { get; set; }
            public string DeletedBy { get; set; } = string.Empty;
        }
    
}
