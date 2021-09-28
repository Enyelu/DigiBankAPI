using DtoMappings.DTO;
using Models;
using System;

namespace DtoMappings.Mappings
{
    public  class UserMapping
    {
        public static User Register(RegisterDTO registerDTO)
        {
            if(registerDTO != null)
            {
                return new User
                {
                    FirstName = registerDTO.FirstName,
                    LastName = registerDTO.LastName,
                    UserName = registerDTO.UserName,
                    PhoneNumber = registerDTO.PhoneNumber,
                    Email = registerDTO.Email,
                    PasswordHash = registerDTO.PasswordHash,
                };
            }
            throw new ArgumentNullException("Fields cannat be null");
           
        }

        public static UserAddress Address(UserAddressDTO userAddressDTO)
        {
            if(userAddressDTO != null)
            {
                return new UserAddress
                {
                    Id = Guid.NewGuid().ToString(),
                    StreetNumber = userAddressDTO.StreetNumber,
                    StreetName = userAddressDTO.StreetName,
                    City = userAddressDTO.City,
                    State = userAddressDTO.State,
                    Country = userAddressDTO.Country,
                    DateCreated = DateTime.Now
                };
            }
            throw new ArgumentNullException("Fields cannot be null");
        }
        public static LoginResponseDTO Login()
        {
            return new LoginResponseDTO
            {
                Status = true,
            };
            throw new ArgumentException("Invalid credentials");
        }
    }
}
