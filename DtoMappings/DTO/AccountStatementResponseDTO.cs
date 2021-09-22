using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoMappings.DTO
{
    public class AccountStatementResponseDTO
    {
        public string TransactionType { get; set; }
        public string Narration { get; set; }
        public double AmountTransacted { get; set; }
        public double DepositorAccountBalance { get; set; }
        public DateTime DateCreated { get; set; }
    }
}

