using BusinessLogic.Interfaces;
using DataBaseConnections;
using DtoMappings.DTO;
using DtoMappings.Mappings;
using Microsoft.AspNetCore.Identity;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task<bool> SaveChanges()
        {
            var response = await _digiBankContext.SaveChangesAsync();
            if (response >= 1)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> AddBankAccount(BankAccount bankAccount)
        {
            var addAccount = await _digiBankContext.BankAccounts.AddAsync(bankAccount);
                return true;
        }
        public async Task<User> GetLoggedInUser(UserAddressDTO userAddressDTO)
        {
            User logedInUser = await _userManager.FindByIdAsync(userAddressDTO.LoggedInUserId);
            return logedInUser;
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
        public async Task<bool> AddAddress(UserAddressDTO userAddressDTO)
        {
            var AddressToRegister = UserMapping.Address(userAddressDTO);
            await _digiBankContext.UsersAddress.AddAsync(AddressToRegister);
          
            var loggedInUser = await GetLoggedInUser(userAddressDTO);
            loggedInUser.UserAddress = AddressToRegister;

            return true;
        }

        public async Task<bool> UpdateUserAsync(UserAddressDTO userAddressDTO)
        {
            var loggedInUser = await GetLoggedInUser(userAddressDTO);
            await _userManager.UpdateAsync(loggedInUser);
            return true;
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
