using DtoMappings.DTO;
using Models;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUserRepo
    {
        Task CreateRole();
        Task<bool> AssignRoleAsync(User user);
        Task<User> CreateUser(RegisterDTO registerDTO);
        Task<User> GetUserByEmail(LoginDTO loginDTO);
        Task<bool> AddAddress(UserAddressDTO userAddressDTO, string loggedInUserId);
        Task<bool> UpdateUserAsync(string loggedInUser);
        Task<bool> ReadPassword(User user, LoginDTO loginDTO);
    }
}
