using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.Models
{
    public class RegRequestDto
    {
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string StateOfResidence { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string LGA { get; set; }
    }
}
