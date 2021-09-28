using BusinessLogic.Interfaces;
using DtoMappings.DTO;
using Models;
using Repository.Interface;
using Repository.Interfaces;
using System;
using System.Threading.Tasks;


namespace BusinessLogic.Implementations
{
    public class TransactionLogic: ITransactionLogic
    {
        private readonly ITransactRepo _transactRepo;
        private readonly IBankAccountRepo _bankAccountRepo;
        public TransactionLogic(ITransactRepo transactRepo, IBankAccountRepo bankAccountRepo)
        {
            _transactRepo = transactRepo;
            _bankAccountRepo = bankAccountRepo;
        }
       
        public bool Transfer(TransactionDepositDTO transactionDTO)
        {
            var response = _bankAccountRepo.GetAccounts(transactionDTO);
            if (transactionDTO.AmountTransacted >= 1 && transactionDTO.AmountTransacted <= 1000000)
            {
                if (response.Depositor.AccountNumber != null)
                {
                    if ((response.Depositor.AccountBalance - transactionDTO.AmountTransacted) >= 500)
                    {
                        if (response.Receiver.AccountNumber != null)
                        {
                            response.Depositor.AccountBalance = response.Depositor.AccountBalance - transactionDTO.AmountTransacted;
                            response.Receiver.AccountBalance = response.Depositor.AccountBalance + transactionDTO.AmountTransacted;
                            _transactRepo.SaveChanges();

                            Models.Transaction transaction = new Models.Transaction
                            {
                                Id = Guid.NewGuid().ToString(),
                                DateCreated = DateTime.Now,
                                DepositorAccountId = response.Depositor.Id,
                                DepositorAccountNumber = response.Depositor.AccountNumber,
                                DepositorAccountBalance = response.Depositor.AccountBalance,
                                ReceiverAccountId = response.Receiver.Id,
                                ReceiverAccountNumber = response.Receiver.AccountNumber,
                                AmountTransacted = transactionDTO.AmountTransacted,
                                TransactionType = "Debit",
                                TransactionStatus = true
                            };
                            _transactRepo.AddTransaction(transaction);
                            _transactRepo.SaveChanges();

                        }
                        throw new ArgumentException("Receivers account number is invalid");
                    }
                    throw new ArgumentOutOfRangeException("Insufficient fund");
                }
                throw new ArgumentException("User account number not found");
            }
            throw new ArgumentException("Amount too small or too large");
        }

        public async Task<bool> AdminDepositAsync(AdminTransactionDTO adminTransactionDTO)
        {
            if (adminTransactionDTO.AmountTransacted >= 1 && adminTransactionDTO.AmountTransacted <= 1000000)
            {
                var account = _bankAccountRepo.GetAccountWithAccountNumber(adminTransactionDTO.AccountNumber);

                if (account.AccountNumber != null)
                {
                    account.AccountBalance = account.AccountBalance + adminTransactionDTO.AmountTransacted;
                    await _transactRepo.SaveChanges();

                     var transaction =  new Transaction
                     {
                            Id = Guid.NewGuid().ToString(),
                            DepositorAccountId = account.Id,
                            DateCreated = DateTime.Now,
                            AmountTransacted = adminTransactionDTO.AmountTransacted,
                            TransactionType = "Credit",
                            TransactionStatus = true
                     };

                     await _transactRepo.AddTransaction(transaction);
                     await _transactRepo.SaveChanges();
                    return true;
                }
                return false;
            }
            return false;
        }
        
        public bool Withdrawal(TransactionWithdrawalDTO transactionDTO)   
        {
           var account = _bankAccountRepo.GetAccountWithUserId(transactionDTO.LoggedInUserId);

            if (account.AccountNumber != null)
            {
                if (transactionDTO.AmountTransacted >= 1 && transactionDTO.AmountTransacted <= 1000000)
                {
                    if ((account.AccountBalance - transactionDTO.AmountTransacted) > 500)
                    {
                        account.AccountBalance = account.AccountBalance - transactionDTO.AmountTransacted;
                        _transactRepo.SaveChanges();

                        if (true)
                        {
                            var transaction =  new Models.Transaction
                            {
                                Id = Guid.NewGuid().ToString(),
                                DepositorAccountNumber = account.AccountNumber,
                                AmountTransacted = transactionDTO.AmountTransacted,
                                TransactionType = "Debit/Withdrawal",
                                DateCreated = DateTime.Now,
                                TransactionStatus = true
                            };
                            _transactRepo.SaveChanges();
                        }
                        throw new Exception("Transaction was not successful");
                    }
                    throw new ArgumentOutOfRangeException("Insufficient balance");
                }
                throw new ArgumentOutOfRangeException("Amount too small or too large");
            }
            throw new ArgumentException("Account not found");
        }

        public bool AdminWithdrawal(AdminTransactionDTO adminTransactionDTO)
        {
            var account = _bankAccountRepo.GetAccountWithAccountNumber(adminTransactionDTO.AccountNumber);

            
            if (account.AccountNumber != null)
            {
                if (adminTransactionDTO.AmountTransacted >= 1)
                {
                    if ((account.AccountBalance - adminTransactionDTO.AmountTransacted) > 500)
                    {
                        account.AccountBalance = account.AccountBalance - adminTransactionDTO.AmountTransacted;
                        _transactRepo.SaveChanges();

                        if (true)
                        {
                            var transaction = new Models.Transaction
                            {
                                Id = Guid.NewGuid().ToString(),
                                DepositorAccountNumber = account.AccountNumber,
                                AmountTransacted = adminTransactionDTO.AmountTransacted,
                                TransactionType = "Debit/Withdrawal",
                                DateCreated = DateTime.Now,
                                TransactionStatus = true
                            };
                            _transactRepo.SaveChanges();
                        }
                        throw new Exception("Transaction was not successful");
                    }
                    throw new ArgumentOutOfRangeException("Insufficient balance");
                }
                throw new ArgumentOutOfRangeException("Amount too small or too large");
            }
            throw new ArgumentException("Account not found");
        }

        public AccountStatementResponseDTO GetTransactionsStatement(string loggedInUserId)
        {
            var transactions = _transactRepo.GetTransactionByUserId(loggedInUserId);

            if (transactions != null)
            {
                foreach (var transaction in transactions)
                {
                    return new AccountStatementResponseDTO
                    {
                        AmountTransacted = transaction.AmountTransacted,
                        DepositorAccountBalance = transaction.DepositorAccountBalance,
                        TransactionType = transaction.TransactionType,
                        Narration = transaction.TransactionType,
                        DateCreated = transaction.DateCreated
                    };
                }
            }
            throw new ArgumentException("Error while fetching data");
        }
        
        public AccountStatementResponseDTO AdminGetTransactionsStatement(string userAccountNumber)
        {

            var transactions = _transactRepo.GetTransactionByAccountNumber(userAccountNumber);

            if (transactions != null)
            {
                foreach (var transaction in transactions)
                {
                    return new AccountStatementResponseDTO
                    {
                        AmountTransacted = transaction.AmountTransacted,
                        DepositorAccountBalance = transaction.DepositorAccountBalance,
                        TransactionType = transaction.TransactionType,
                        Narration = transaction.TransactionType,
                        DateCreated = transaction.DateCreated
                    };
                }
            }
            throw new ArgumentException("Account not found");
        }

        public async Task<bool> SaveDbChanges()
        {
            var saveResult = await _transactRepo.SaveChanges();
            if (saveResult)
            {
                return true;
            }
            return false;
        }
    }
}
