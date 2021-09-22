using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Transaction
    {
        [Key]
        public string Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string DepositorAccountNumber { get; set; }
        public string DepositorAccountId { get; set; }
        public string ReceiverAccountNumber { get; set; }
        public string ReceiverAccountId { get; set; }
        public double AmountTransacted { get; set; }
        public string TransactionType { get; set; }
        public bool TransactionStatus { get; set; }
        public double DepositorAccountBalance { get; set; }
        public BankAccount BankAccount { get; set; }
    }
}
