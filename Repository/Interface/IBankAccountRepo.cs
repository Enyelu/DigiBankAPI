using DtoMappings.DTO;
using Models;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IBankAccountRepo
    {
        Task<bool> AddBankAccount(BankAccount bankAccount);
        BankAccountsResponse GetAccounts(TransactionDepositDTO transactionDTO);
        BankAccount GetAccountWithUserId(string userId);
        BankAccount GetAccountWithAccountNumber(string userAccount);
    }
}
