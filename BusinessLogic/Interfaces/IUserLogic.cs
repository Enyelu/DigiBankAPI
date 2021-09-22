using DtoMappings.DTO;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IUserLogic
    {
        Task<RegisterResponseDTO> RegisterUserAsync(RegisterDTO registerDTO);
        Task<AddressResponseDTO> UserAddressAsync(UserAddressDTO userAddressDTO);
        Task<LoginResponseDTO> LoginUser(LoginDTO loginDTO);
    }
}
    
