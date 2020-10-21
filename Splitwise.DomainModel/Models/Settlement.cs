using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Splitwise.DomainModel.Models
{
    public class Settlement
    {
        public int SettlementId { get; set; }
        public int ExpenseId { get; set; }
        [ForeignKey("ExpenseId")]
        public Expense Expense { get; set; }
        public string Payer { get; set; }
        [ForeignKey("Payer")]
        public ApplicationUser PayerUser { get; set; }
        public string Payee { get; set; }
        [ForeignKey("Payee")]
        public ApplicationUser PayeeUser { get; set; }
        public DateTime Date { get; set; }
        public float Amount { get; set; }
        public int? GroupId { get; set; }
        [ForeignKey("GroupId")]
        public Group Group { get; set; }

    }
}
