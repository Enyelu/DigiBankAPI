using BusinessLogic.Interfaces;
using DtoMappings.DTO;
using DtoMappings.Mappings;
using Models;
using System;
using System.Threading.Tasks;
using Utilities.TokenGeneration;

namespace BusinessLogic.Implementations
{
    public class UserLogic:IUserLogic
    {
        private readonly ITransactionLogic _transactionLogic;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IUserRepo _userRepo;
     
        public UserLogic(ITransactionLogic transactionLogic, ITokenGenerator tokenGenerator, IUserRepo userRepo)
        {
            _transactionLogic = transactionLogic;
            _tokenGenerator = tokenGenerator;
            _userRepo = userRepo;
        }

        public async Task<RegisterResponseDTO> RegisterUserAsync(RegisterDTO registerDTO)
        {
            await _userRepo.CreateRole();
           
            var user = await _userRepo.CreateUser(registerDTO);
                
            if(user == null)
            {
                    throw new NotImplementedException("Error!:Account not created");
            }

            var assignRole = _userRepo.AssignRoleAsync(user);
                
            if(assignRole.IsCompletedSuccessfully)
            {
                var bankAccount = BankAccountMapping.CreateAccount(registerDTO);
                bankAccount.User = user;

                await _userRepo.AddBankAccount(bankAccount);
                await _userRepo.SaveChanges();

                var transaction = new AdminTransactionDTO
                {
                    AccountNumber = bankAccount.AccountNumber,
                    AmountTransacted = registerDTO.DepositAmount
                };
                   
                //Deposit method !!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                await _transactionLogic.AdminDepositAsync(transaction);
            }

            return new RegisterResponseDTO
            {
                Description = $"Account creation for {user.FirstName}  {user.LastName} was successful",
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                DateCreated = user.DateCreated,
            };
            
            throw new Exception("Account creation unsuccessful");
        }

        //User adds address after registration and login in.
        public async Task<AddressResponseDTO> UserAddressAsync(UserAddressDTO userAddressDTO)
        {
            User logedInUser = await _userRepo.GetLoggedInUser(userAddressDTO);

            if (logedInUser != null)
            {
               var addAddress = await _userRepo.AddAddress(userAddressDTO);
                if(addAddress == true)
                {
                   var saveAddress = await _userRepo.SaveChanges();
                    if(saveAddress == true)
                    {
                        var createUserAddress = await _userRepo.UpdateUserAsync(userAddressDTO);
                        if(createUserAddress == true)
                        {
                            return new AddressResponseDTO
                            {
                                Status = true,
                            };
                        }
                        throw new Exception("Address was not added to user");
                    }
                    throw new Exception("Address was not saved");
                }
                throw new ArgumentException("Address was not created");
            }
            throw new ArgumentNullException("User is not valid");
            
        }
        public async Task <LoginResponseDTO> LoginUser(LoginDTO loginDTO)
        {
            User user = await _userRepo.GetUserByEmail(loginDTO);
            if(user != null)
            {
                var response = await _userRepo.ReadPassword(user, loginDTO);
                if ( response == true)
                {
                    var result = new LoginResponseDTO { Status = true, Token = await _tokenGenerator.GenerateTokenAsync(user) };
                    return result;
                }
                throw new ArgumentException("User does not exist");
               
                throw new ArgumentException("Email or password incorrect");
            }
            throw new ArgumentException("Email or password incorrect");
        }
    }
}
