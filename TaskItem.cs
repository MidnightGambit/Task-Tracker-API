using System.ComponentModel.DataAnnotations;

namespace TaskTrackerApi.Models
{
    // Core entity mapped to the "Tasks" table by EF Core.
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        public bool IsComplete { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? DueDate { get; set; }
    }
}
