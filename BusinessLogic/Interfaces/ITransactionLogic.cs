using DtoMappings.DTO;
using DtoMappings.DTO.userResponseDTO;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface ITransactionLogic
    {
        bool Transfer(TransactionDepositDTO transactionDepositDTO);
        Task<bool> AdminDepositAsync(AdminTransactionDTO adminTransactionDTO);
        bool Withdrawal(TransactionWithdrawalDTO transactionWithdrawalDTO);
        bool AdminWithdrawal(AdminTransactionDTO adminTransactionDTO);
        AccountStatementResponseDTO GetTransactionsStatement(string loggedInUserId);
        AccountStatementResponseDTO AdminGetTransactionsStatement(string userAccountNumber);
        Task<bool> SaveDbChanges();
    }
}
