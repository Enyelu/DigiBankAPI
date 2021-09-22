using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class User : IdentityUser
    {
        [Key]
        public override string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime DateCreated { get; set; }
        public UserAddress UserAddress { get; set; }
    }
}
