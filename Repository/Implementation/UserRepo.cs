using DataBaseConnections;
using DtoMappings.DTO;
using DtoMappings.Mappings;
using Microsoft.AspNetCore.Identity;
using Models;
using Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace BusinessLogic.Implementations
{
    public class UserRepo : IUserRepo
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly DigiBankContext _digiBankContext;

        public UserRepo(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, DigiBankContext digiBankContext)
        {
            _userManager = userManager;
            _rolemanager = roleManager;
            _digiBankContext = digiBankContext;
        }

        public async Task CreateRole()
        {
            await _rolemanager.CreateAsync(new IdentityRole { Name = "Regular" });
        }
        public async Task<bool> AssignRoleAsync(User user)
        {
            var assign = await _userManager.AddToRoleAsync(user, "Regular");
            if(assign.Succeeded)
            {
                return true;
            }
            return false;
        }
        public async Task<User> CreateUser(RegisterDTO registerDTO)
        {
            var user = UserMapping.Register(registerDTO);
            user.DateCreated = DateTime.Now;
            IdentityResult create = await _userManager.CreateAsync(user, registerDTO.PasswordHash);

            if(create.Succeeded == true)
            {
                return user;
            }
            return null;
        }

        public async Task<User> GetUserByEmail(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if(user != null)
            {
                return user;
            }
            return null;
        }
        public async Task<bool> AddAddress(UserAddressDTO userAddressDTO, string loggedInUserId)
        {
            User loggedInUser = await _userManager.FindByIdAsync(loggedInUserId);
            var AddressToRegister = UserMapping.Address(userAddressDTO);
            await _digiBankContext.UsersAddress.AddAsync(AddressToRegister);
          
            loggedInUser.UserAddress = AddressToRegister;
            if(loggedInUser.UserAddress != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateUserAsync(string loggedInUserId)
        {
            User loggedInUser = await _userManager.FindByIdAsync(loggedInUserId);
            var result = await _userManager.UpdateAsync(loggedInUser);
            if(result != null)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> ReadPassword(User user, LoginDTO loginDTO)
        {
            var response = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
            if(response == true)
            {
                return true;
            }
            return false;
        }
    }
}
