using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.Models
{
    public class LoginReqDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
