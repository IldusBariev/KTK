using System.ComponentModel.DataAnnotations;

namespace BlazorApp2.Request
{
    public class User
    {

        [Required (ErrorMessage = "Логин не может быть пустым")]
        public string Username { get; set; }
        [Required (ErrorMessage = "Пароль не можеть быть пустым")]
        public string Password { get; set; }
    }
}
