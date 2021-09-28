using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ITransactRepo
    {
        Task<bool> SaveChanges();
        IEnumerable<Transaction> GetTransactionByUserId(string AccountNumber);
        IEnumerable<Transaction> GetTransactionByAccountNumber(string AccountNumber);
        Task<bool> AddTransaction(Models.Transaction transaction);
    }
}
