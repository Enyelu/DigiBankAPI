using BusinessLogic.Interfaces;
using DataBaseConnections;
using DtoMappings.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Utilities;

namespace BusinessLogic.Implementations
{
    public class BankAccountLogic : IBankAccountLogic
    {
        private readonly DigiBankContext _digiBankContext;
        public BankAccountLogic(DigiBankContext digiBankContext)
        {
            _digiBankContext = digiBankContext;
        }
        public AccountBalanceResponseDTO GetAccountBalance()
        {
            var loggedInUserId = LoggedUser.LoggedInUserId;
            var user = _digiBankContext.BankAccounts.FirstOrDefault(x => x.User.Id == loggedInUserId);

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
            throw new ArgumentException("Account not found");
        }

        public AccountBalanceResponseDTO AdminGetAccountBalance(string userAccountNumber)
        {
            var user = _digiBankContext.BankAccounts.FirstOrDefault(x => x.AccountNumber == userAccountNumber);

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
            throw new ArgumentException("Account not found");
        }
        public AccountStatementResponseDTO GetAccountStatement()
        {
            var loggedInUserId = LoggedUser.LoggedInUserId;
            var user = _digiBankContext.Transactions.Include(x => x.BankAccount).FirstOrDefault(e => e.BankAccount.User.Id == loggedInUserId);

            if (user != null)
            {
                return new AccountStatementResponseDTO
                {
                    AmountTransacted = user.AmountTransacted,
                    DepositorAccountBalance = user.DepositorAccountBalance,
                    TransactionType = user.TransactionType,
                    Narration = user.TransactionType,
                    DateCreated = user.DateCreated
                };
            }
            throw new ArgumentException("Account not found");
        }

        public AccountStatementResponseDTO AdminGetAccountStatement(string userAccountNumber)
        {

            var user = _digiBankContext.Transactions.Include(x => x.BankAccount).FirstOrDefault(e => e.BankAccount.AccountNumber == userAccountNumber);

            if (user != null)
            {
                return new AccountStatementResponseDTO
                {
                    AmountTransacted = user.AmountTransacted,
                    DepositorAccountBalance = user.DepositorAccountBalance,
                    TransactionType = user.TransactionType,
                    Narration = user.TransactionType,
                    DateCreated = user.DateCreated
                };
            }
            throw new ArgumentException("Account not found");
        }
    }
}

