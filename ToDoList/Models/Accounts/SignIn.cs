using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models.Accounts
{
    public class SignIn
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
