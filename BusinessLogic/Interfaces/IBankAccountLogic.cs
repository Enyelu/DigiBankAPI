using DtoMappings.DTO;

namespace BusinessLogic.Interfaces
{
    public interface IBankAccountLogic
    {
        AccountBalanceResponseDTO GetAccountBalance();
        AccountBalanceResponseDTO AdminGetAccountBalance(string userAccountNumber);
        AccountStatementResponseDTO GetAccountStatement();
        AccountStatementResponseDTO AdminGetAccountStatement(string userAccountNumber);
    }
}
