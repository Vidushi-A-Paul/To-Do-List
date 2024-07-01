using System.ComponentModel.DataAnnotations;

namespace ToDoListApp.Models
{
    public class ToDoItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Task")]
        public string? TaskName { get; set; }

        public bool IsCompleted { get; set; }
    }
}
