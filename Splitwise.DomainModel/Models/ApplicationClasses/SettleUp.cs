using System;
using System.Collections.Generic;
using System.Text;

namespace Splitwise.DomainModel.Models.ApplicationClasses
{
    public class SettleUp
    {
        public int ExpenseId { get; set; }
        public string Payer { get; set; }
        public string Payee { get; set; }
        public float Amount { get; set; }
        public DateTime Date { get; set; }
        public int? GroupId { get; set; }
    }
}
