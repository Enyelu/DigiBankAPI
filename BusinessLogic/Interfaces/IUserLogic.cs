using DtoMappings.DTO;
using Models;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IUserLogic
    {
        Task<RegisterResponseDTO> RegisterUserAsync(RegisterDTO registerDTO);
        Task<AddressResponseDTO> UserAddressAsync(UserAddressDTO userAddressDTO, string logedInUser);
        Task<LoginResponseDTO> LoginUser(LoginDTO loginDTO);
    }
}
    
