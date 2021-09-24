using System;
using System.ComponentModel.DataAnnotations;

namespace DemoLogin.Domain.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter de 3 a 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter de 3 a 60 caracteres")]
        public string Title { get; set; }

        [MaxLength(1204, ErrorMessage = "Este campo deve conter no máximo 1024 caracteres")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
        public decimal Price { get; set; }

        public string UserId { get; set; }
    }
}
