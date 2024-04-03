
using System.ComponentModel.DataAnnotations;

namespace LyncasSales.Domain.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo email é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo email é inválido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatório")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Lembrar-me")]
        public bool RememberMe { get; set; }
    }
}
