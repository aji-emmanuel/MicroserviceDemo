using System;

namespace UserMicroservice.Models
{
    public class UserDto
    {
        public string Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string StateOfResidence { get; set; }
        public string LGA { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
