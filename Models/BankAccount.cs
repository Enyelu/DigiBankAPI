using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class BankAccount
    {
        [Key]
        public string Id { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public double AccountBalance { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
