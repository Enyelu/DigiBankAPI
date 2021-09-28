using System.ComponentModel.DataAnnotations;

namespace DtoMappings.DTO
{
    public class UserAddressDTO
    {
        [Required]
        public string StreetNumber { get; set; }
        [Required]
        public string StreetName { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        public string LoggedInUserId { get; set; }
    }
}
