using System.ComponentModel.DataAnnotations;

namespace SytePortfolio
{
    public class CreateUser
    {
        public int IdUser { get; set; }

        [StringLength(30, MinimumLength = 5, ErrorMessage = "Длина логина должна быть от 5 до 30 символов")]
        [Required(ErrorMessage = "Поля не могут быть пустыми")]
        public string LoginUser { get; set; }

        public string password { get; set; }

        [Required(ErrorMessage = "Поля не могут быть пустыми")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Длина пароля должна быть от 5 до 30 символов")]
        [DataType(DataType.Password)]
        public string password1 { get; set; }

        [Required(ErrorMessage = "Поля не могут быть пустыми")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Длина пароля должна быть от 5 до 30 символов")]
        [Compare("password1", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string password2 { get; set; }
    }
}
