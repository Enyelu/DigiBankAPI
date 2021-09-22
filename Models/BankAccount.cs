using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
