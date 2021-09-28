using BusinessLogic.Interfaces;
using DtoMappings.DTO;
using Models;
using Repository.Interface;
using System;
using System.Threading.Tasks;

namespace BusinessLogic.Implementations
{
    public class BankAccountLogic : IBankAccountLogic
    {
        private readonly IBankAccountRepo _bankAccountRepo;

        public BankAccountLogic(IBankAccountRepo bankAccountRepo)
        {
            _bankAccountRepo = bankAccountRepo;
        }

       public async Task<bool> AddBankAccount(BankAccount bankAccount)
        {
           var addAccount = await _bankAccountRepo.AddBankAccount(bankAccount);
            if(addAccount == true)
            {
                return true;
            }
            return false;
        }
        
        public AccountBalanceResponseDTO GetAccountBalance(string loggedInUserId)
        {
            var user = _bankAccountRepo.GetAccountWithUserId(loggedInUserId);

            if (user != null)
            {
                return new AccountBalanceResponseDTO
                {
                    AccountName = user.AccountName,
                    AccountNumber = user.AccountNumber,
                    Balance = user.AccountBalance,
                    CheckStatus = true,
                    Message = $"{user.AccountName} thanks for banking with DigiBank!"
                };
            }
            throw new ArgumentException("Internal error");
        }

        public AccountBalanceResponseDTO AdminGetAccountBalance(string userAccountNumber)
        {
            var user = _bankAccountRepo.GetAccountWithAccountNumber(userAccountNumber);

            if (user != null)
            {
                return new AccountBalanceResponseDTO
                {
                    AccountName = user.AccountName,
                    AccountNumber = user.AccountNumber,
                    Balance = user.AccountBalance,
                    CheckStatus = true,
                    Message = $"{user.AccountName} thanks for banking with DigiBank!"
                };
            }
            throw new ArgumentException("Internal error");
        }
    }
}

