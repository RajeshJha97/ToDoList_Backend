using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models.Accounts
{
    public class SiginIn
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
