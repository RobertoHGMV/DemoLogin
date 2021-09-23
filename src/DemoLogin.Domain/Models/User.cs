using System;
using System.ComponentModel.DataAnnotations;

namespace DemoLogin.Domain.Models
{
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid().ToString().Substring(0, 8);
        }

        public string Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter de 3 a 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter de 3 a 60 caracteres")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter de 3 a 20 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter de 3 a 20 caracteres")]
        public string Password { get; set; }

        public string Role { get; set; }

        public bool Authenticate(string username, string password)
        {
            //var encrypted = PasswordHelper.Encrypt(password);

            if (UserName == username && Password == password)
                return true;

            //AddNotification("User", "Usuário ou senha inválidos");
            return false;
        }
    }
}
