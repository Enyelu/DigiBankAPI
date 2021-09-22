using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoMappings.DTO
{
    public class TransactionDepositDTO
    {
        public string ReceiverAccountNumber { get; set; }
        public double AmountTransacted { get; set; }
        public string userId { get; set; }
    }
}
