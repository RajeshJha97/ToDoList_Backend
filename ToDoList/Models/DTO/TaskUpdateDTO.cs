using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models.DTO
{
    public class TaskUpdateDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        
        [Required]
        public string Category { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
