using DataBaseConnections;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Implementations
{
    public class TransactRepo: ITransactRepo
    {
        private readonly DigiBankContext _digiBankContext;
        public TransactRepo(DigiBankContext digiBankContext)
        {
            _digiBankContext = digiBankContext;
        }

        public async Task<bool> AddTransaction(Transaction transaction)
        {
            var addTransaction = await _digiBankContext.Transactions.AddAsync(transaction);
            
            if(addTransaction != null)
            {
                return true;
            }
            return false;
        }
        public IEnumerable<Transaction> GetTransactionByUserId(string userId)
        {
            var transaction = _digiBankContext.Transactions.Include(x => x.BankAccount).ThenInclude(z => z.User).Where(x => x.Id == userId);

            if (transaction != null)
            {
                return transaction;
            }
            return null;
        }

        public IEnumerable<Transaction> GetTransactionByAccountNumber(string AccountNumber)
        {
            var transaction = _digiBankContext.Transactions.Include(x => x.BankAccount).Where(x => x.BankAccount.AccountNumber == AccountNumber);
           
            if(transaction != null)
            {
                return transaction;
            }
            return null;
        }

        public async Task<bool> SaveChanges()
        {
            var response = await _digiBankContext.SaveChangesAsync();
            if (response >= 1)
            {
                return true;
            }
            return false;
        }
    }
}

