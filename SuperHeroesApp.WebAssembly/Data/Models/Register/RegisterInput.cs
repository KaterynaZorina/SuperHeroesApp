using System.ComponentModel.DataAnnotations;

namespace SuperHeroesApp.WebAssembly.Data.Models.Register
{
    public class RegisterInput
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string Name { get; set; }
        
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "ConfirmPassword and Password should match")]
        public string ConfirmPassword { get; set; }
    }
}