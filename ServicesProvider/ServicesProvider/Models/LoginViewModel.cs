using System.ComponentModel.DataAnnotations;

namespace ServicesProvider.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Поле 'Email' обязательно для заполнения")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле 'Пароль' обязательно для заполнения")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
