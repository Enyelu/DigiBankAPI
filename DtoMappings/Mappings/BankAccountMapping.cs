using DtoMappings.DTO;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoMappings.Mappings
{
    public class BankAccountMapping
    {
        public static BankAccount CreateAccount(RegisterDTO registerDTO)
        {
            Random random = new Random();
            string partAccountNum1 = "2191";

            BankAccount bankAccount = new BankAccount()
            {
                AccountBalance = 0,
                DateCreated = DateTime.Now,
                AccountName = $"{registerDTO.FirstName} {registerDTO.LastName}",
                AccountNumber = partAccountNum1 + random.Next(100000, 999999),
                Id = Guid.NewGuid().ToString(),
            };
            return bankAccount;
        }
    }
}
