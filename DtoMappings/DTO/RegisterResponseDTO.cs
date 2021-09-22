using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoMappings.DTO
{
    public class RegisterResponseDTO
    {
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
