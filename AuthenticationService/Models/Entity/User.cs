using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string StateOfResidence { get; set; }
        [Required]
        public string LGA { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
