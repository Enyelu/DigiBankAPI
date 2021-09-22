using DtoMappings.DTO;
using Models;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IUserRepo
    {
        Task CreateRole();
        Task<bool> AssignRoleAsync(User user);
        Task<User> CreateUser(RegisterDTO registerDTO);
        Task<bool> SaveChanges();
        Task<bool> AddBankAccount(BankAccount bankAccount);
        Task<User> GetLoggedInUser(UserAddressDTO userAddressDTO);
        Task<bool> AddAddress(UserAddressDTO userAddressDTO);
        Task<bool> UpdateUserAsync(UserAddressDTO userAddressDTO);
        Task<User> GetUserByEmail(LoginDTO loginDTO);
        Task<bool> ReadPassword(User user, LoginDTO loginDTO);
    }
        
}
