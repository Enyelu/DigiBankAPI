using Models;

namespace DtoMappings.DTO
{
    public  class BankAccountsResponse
    {
        public  BankAccount Depositor { get; set; }
        public  BankAccount Receiver { get; set; }
    }
}
