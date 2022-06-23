using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Transaction : BaseEntity // Inheriting from Base Entity class
    {
        //Transaction type
        public TransactionType TransactionType { get; set; }

        //When transaction was recorded
        public DateTime TransactionDate { get; set; }

        //Amount of transaction
        public decimal TransactionAmount { get; set; }

        //Associcated acocunt of that transaction
        public Account Account { get; set; }
    }

    // Two posible types of an Trasaction
    public enum TransactionType
    {
        Deposit = 0,    // When money is added to account
        Withdraw = 1    // When money is subtracted from account
    }
}