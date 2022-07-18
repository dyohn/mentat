using System.ComponentModel.DataAnnotations;

namespace Mentat.LoginRegister.Areas.Identity.Data
{
    public class User : ApplicationUser
    {
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string UsrName { get; set; } = null!;

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string UsrEmail { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
