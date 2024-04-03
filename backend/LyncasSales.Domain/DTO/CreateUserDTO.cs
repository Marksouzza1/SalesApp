using System.ComponentModel.DataAnnotations;

namespace LyncasSales.Domain.DTO
{
    public class CreateUserDTO
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("Password", ErrorMessage = "As senhas não conferem.")]
        public string? ConfirPassword { get; set; }
    }
}
