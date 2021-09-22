using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoMappings.DTO
{
    public class TransactionWithdrawalDTO
    {
        public double AmountTransacted { get; set; }
        public string LoggedInUserId { get; set; }
    }
}
