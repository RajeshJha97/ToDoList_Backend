using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models.DTO
{
    public class TaskCreateDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Category { get; set; }
    }
}
