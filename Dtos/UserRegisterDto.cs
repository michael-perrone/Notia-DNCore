using System.ComponentModel.DataAnnotations;

namespace Notia.Dtos
{
    public class UserRegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email {get;set;}
        [Required]
        public string Name {get;set;}

        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 Characters")]
        public string Password {get;set;}
    }
}