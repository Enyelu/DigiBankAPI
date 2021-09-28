namespace DtoMappings.DTO
{
    public class AccountBalanceResponseDTO
    {
        public string AccountName { get; set; }
        public string Message { get; set; }
        public string AccountNumber { get; set; }
        public double Balance { get; set; }
        public bool CheckStatus { get; set; }
    }
}
