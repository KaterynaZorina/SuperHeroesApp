using System.ComponentModel.DataAnnotations;

namespace SuperHeroesApp.WebAssembly.Data.Models.Login
{
    public class LoginInput
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}