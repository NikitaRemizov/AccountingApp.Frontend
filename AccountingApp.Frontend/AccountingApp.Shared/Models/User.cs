using System.ComponentModel.DataAnnotations;

namespace AccountingApp.Shared.Models
{
    public class User
    {
        [Required(ErrorMessage = "The field Email must be set")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "The field Password must be set")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "The password must be at least 8 characters long")]
        [MaxLength(50, ErrorMessage = "The password must be less than 50 characters long")]
        public string Password { get; set; }
    }
}
