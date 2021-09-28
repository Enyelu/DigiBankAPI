using DataBaseConnections;
using DtoMappings.DTO;
using Models;
using Repository.Interface;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class BankAccountRepo : IBankAccountRepo
    {
        private readonly DigiBankContext _digiBankContext;
        public BankAccountRepo(DigiBankContext digiBankContext)
        {
            _digiBankContext = digiBankContext;
        }

        public async Task<bool> AddBankAccount(BankAccount bankAccount)
        {
            var addAccount = await _digiBankContext.BankAccounts.AddAsync(bankAccount);
            return true;
        }

        public BankAccountsResponse GetAccounts(TransactionDepositDTO transactionDTO)
        {
            BankAccountsResponse transaction = new BankAccountsResponse();
            transaction.Depositor = _digiBankContext.BankAccounts.FirstOrDefault(x => x.User.Id == transactionDTO.userId);
            transaction.Receiver = _digiBankContext.BankAccounts.FirstOrDefault(x => x.AccountNumber == transactionDTO.ReceiverAccountNumber);
            
            return transaction;
        }

        public BankAccount GetAccountWithUserId(string userId)
        {
            BankAccount loggedUserAccount = _digiBankContext.BankAccounts.FirstOrDefault(x => x.User.Id == userId);

            if(loggedUserAccount != null)
            {
                return loggedUserAccount;
            }
            return null;
        }

        public BankAccount GetAccountWithAccountNumber(string accountNumber)
        {
            BankAccount UserAccount = _digiBankContext.BankAccounts.FirstOrDefault(x => x.AccountNumber == accountNumber);

            if(UserAccount != null)
            {
                return UserAccount;
            }
            return null;
        }
    }
}
