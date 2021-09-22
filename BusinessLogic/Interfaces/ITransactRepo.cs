using DtoMappings.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessLogic.Interfaces
{
    public interface ITransactRepo
    {
        
        TransactionResponseDTO GetAccounts(TransactionDepositDTO transactionDTO);
        Task<bool> SaveChanges();
        Task<bool> AddTransaction(Models.Transaction transaction);
        AdminDepositResponseDTO GetAccountWithAccountNumber(AdminTransactionDTO adminDepositDTO);
        AdminDepositResponseDTO GetAccountWithId(TransactionWithdrawalDTO transactionDTO);
    }
}
