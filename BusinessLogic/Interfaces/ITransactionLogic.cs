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
    }
}
