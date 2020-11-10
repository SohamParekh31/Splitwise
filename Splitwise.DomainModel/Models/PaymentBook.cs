using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Splitwise.DomainModel.Models
{
    public class PaymentBook
    {
        public int PaymentBookId { get; set; }
        public string Payer { get; set; }
        [ForeignKey("Payer")]
        public ApplicationUser PayerUser { get; set; }
        public string Payee { get; set; }
        [ForeignKey("Payee")]
        public ApplicationUser PayeeUser { get; set; }
        public float Paid_Amount { get; set; }
        public int? SettlementId { get; set; }
        [ForeignKey("SettlementId")]
        public Settlement Settlement { get; set; }
    }
}
