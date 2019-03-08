using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OneDayFlat.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не вказано ім'я!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не вказано Email!")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не вказано номер телефону!")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Не вказано пароль!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введено невірно!")]
        public string ConfirmPassword { get; set; }
    }
}
