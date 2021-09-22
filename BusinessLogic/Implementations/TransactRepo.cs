using BusinessLogic.Interfaces;
using DataBaseConnections;
using DtoMappings.DTO;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Implementations
{
    public class TransactRepo: ITransactRepo
    {
        private readonly DigiBankContext _digiBankContext;
        public TransactRepo(DigiBankContext digiBankContext)
        {
            _digiBankContext = digiBankContext;
        }
         
        public TransactionResponseDTO GetAccounts(TransactionDepositDTO transactionDTO)
        {
            TransactionResponseDTO transaction = new TransactionResponseDTO();
            transaction.Depositor = _digiBankContext.BankAccounts.FirstOrDefault(x => x.User.Id == transactionDTO.userId);
            transaction.Receiver = _digiBankContext.BankAccounts.FirstOrDefault(x => x.AccountNumber == transactionDTO.ReceiverAccountNumber);

            return transaction;
        }
        public AdminDepositResponseDTO GetAccountWithAccountNumber(AdminTransactionDTO adminDepositDTO)
        {
            return new AdminDepositResponseDTO { Depositor = _digiBankContext.BankAccounts.FirstOrDefault(x => x.AccountNumber == adminDepositDTO.AccountNumber) };
        }

        public async Task<bool> AddTransaction(Models.Transaction transaction)
        {
            var addTransaction = await _digiBankContext.Transactions.AddAsync(transaction);
            return true;
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

        public AdminDepositResponseDTO GetAccountWithId(TransactionWithdrawalDTO transactionDTO)
        {
            return new AdminDepositResponseDTO { Depositor = _digiBankContext.BankAccounts.FirstOrDefault(x => x.User.Id == transactionDTO.LoggedInUserId) };
        }
    }
}

