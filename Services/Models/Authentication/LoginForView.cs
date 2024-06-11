
using System.ComponentModel.DataAnnotations;


namespace Services.Models.Authentication
{
    public class LoginForView
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; }
    }
}
