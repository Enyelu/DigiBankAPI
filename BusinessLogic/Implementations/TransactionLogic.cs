using BusinessLogic.Interfaces;
using DtoMappings.DTO;
using System;
using System.Threading.Tasks;


namespace BusinessLogic.Implementations
{
    public class TransactionLogic: ITransactionLogic
    {
        private readonly ITransactRepo _transact;
        public TransactionLogic(ITransactRepo transact)
        {
            _transact = transact;
        }
       
        public bool Transfer(TransactionDepositDTO transactionDTO)
        {
            var response = _transact.GetAccounts(transactionDTO);
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
                            _transact.SaveChanges();

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
                            _transact.AddTransaction(transaction);
                            _transact.SaveChanges();

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
                var response = _transact.GetAccountWithAccountNumber(adminTransactionDTO);

                if (response.Depositor.AccountNumber != null)
                {
                    response.Depositor.AccountBalance = response.Depositor .AccountBalance + adminTransactionDTO.AmountTransacted;
                    await _transact.SaveChanges();

                     var transaction =  new Models.Transaction
                     {
                            Id = Guid.NewGuid().ToString(),
                            DepositorAccountId = response.Depositor.Id,
                            DateCreated = DateTime.Now,
                            AmountTransacted = adminTransactionDTO.AmountTransacted,
                            TransactionType = "Credit",
                            TransactionStatus = true
                     };

                     await _transact.AddTransaction(transaction);
                     await _transact.SaveChanges();
                    return true;
                }
                return false;
            }
            return false;
        }
        
        public bool Withdrawal(TransactionWithdrawalDTO transactionDTO)   
        {
           var response =  _transact.GetAccountWithId(transactionDTO);

            if (response.Depositor.AccountNumber != null)
            {
                if (transactionDTO.AmountTransacted >= 1 && transactionDTO.AmountTransacted <= 1000000)
                {
                    if ((response.Depositor.AccountBalance - transactionDTO.AmountTransacted) > 500)
                    {
                        response.Depositor.AccountBalance = response.Depositor.AccountBalance - transactionDTO.AmountTransacted;
                        _transact.SaveChanges();

                        if (true)
                        {
                            var transaction =  new Models.Transaction
                            {
                                Id = Guid.NewGuid().ToString(),
                                DepositorAccountNumber = response.Depositor.AccountNumber,
                                AmountTransacted = transactionDTO.AmountTransacted,
                                TransactionType = "Debit/Withdrawal",
                                DateCreated = DateTime.Now,
                                TransactionStatus = true
                            };
                            _transact.SaveChanges();
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
            var response = _transact.GetAccountWithAccountNumber(adminTransactionDTO);

            
            if (response.Depositor.AccountNumber != null)
            {
                if (adminTransactionDTO.AmountTransacted >= 1)
                {
                    if ((response.Depositor.AccountBalance - adminTransactionDTO.AmountTransacted) > 500)
                    {
                        response.Depositor.AccountBalance = response.Depositor.AccountBalance - adminTransactionDTO.AmountTransacted;
                        _transact.SaveChanges();

                        if (true)
                        {
                            var transaction = new Models.Transaction
                            {
                                Id = Guid.NewGuid().ToString(),
                                DepositorAccountNumber = response.Depositor.AccountNumber,
                                AmountTransacted = adminTransactionDTO.AmountTransacted,
                                TransactionType = "Debit/Withdrawal",
                                DateCreated = DateTime.Now,
                                TransactionStatus = true
                            };
                            _transact.SaveChanges();
                        }
                        throw new Exception("Transaction was not successful");
                    }
                    throw new ArgumentOutOfRangeException("Insufficient balance");
                }
                throw new ArgumentOutOfRangeException("Amount too small or too large");
            }
            throw new ArgumentException("Account not found");
        }
    }
}
