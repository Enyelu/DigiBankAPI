using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoMappings.DTO
{
    public  class TransactionResponseDTO
    {
        public  BankAccount Depositor { get; set; }
        public  BankAccount Receiver { get; set; }
    }
}
