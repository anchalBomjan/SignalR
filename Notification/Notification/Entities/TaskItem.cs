using System.ComponentModel.DataAnnotations;

namespace Notification.Entities
{
    public class TaskItem
    {

      
            [Key]
            public Guid Id { get; set; }

            [Required]
            [MaxLength(200)]
            public string Title { get; set; } = string.Empty;

            [MaxLength(1000)]
            public string Description { get; set; } = string.Empty;

            public DateTime CreatedAt { get; set; }

            [Required]
            [MaxLength(100)]
            public string CreatedBy { get; set; } = string.Empty;

            public DateTime? UpdatedAt { get; set; }

            [MaxLength(100)]
            public string? UpdatedBy { get; set; }

            public bool IsDeleted { get; set; }
        
    }
}
