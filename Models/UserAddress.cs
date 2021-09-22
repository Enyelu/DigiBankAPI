using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class UserAddress
    {
        [Key]
        public string Id { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
