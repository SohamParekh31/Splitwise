    using System;
using System.Collections.Generic;
using System.Text;

namespace Splitwise.DomainModel.Models.ApplicationClasses
{
    public class AddExpense
    {
        public List<string> Email { get; set; }
        public string  Description { get; set; }
        public int? GroupId { get; set; }
        public float Amount { get; set; }
        public string SplitType { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Date { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsSettled { get; set; }
        public List<UserExpenseMapper> PaidBy { get; set; }
    }
}
