using DtoMappings.DTO;
using Models;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IBankAccountLogic
    {
        AccountBalanceResponseDTO GetAccountBalance(string loggedInUserId);
        AccountBalanceResponseDTO AdminGetAccountBalance(string userAccountNumber);
        Task<bool> AddBankAccount(BankAccount bankAccount);
    }
}
