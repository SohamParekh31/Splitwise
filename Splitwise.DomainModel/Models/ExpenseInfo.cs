using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Splitwise.DomainModel.Models
{
    public class ExpenseInfo
    {
        public int ExpenseInfoId { get; set; }
        public int ExpenseId { get; set; }
        [ForeignKey("ExpenseId")]
        public Expense Expense { get; set; }
        public float Paid_Amount { get; set; }
        public float Borrow_Amount { get; set; }
        public float Lent_Amount { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser user { get; set; }
        public float Share_Amount { get; set; }
    }
}
