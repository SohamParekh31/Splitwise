using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Splitwise.DomainModel.Models
{
    public class Expense
    {
        public int ExpenseId { get; set; }
        public string Description { get; set; }
        public float Amount { get; set; }
        public string SplitType { get; set; }
        public int? GroupId { get; set; }
        [ForeignKey("GroupId")]
        public Group Group { get; set; }
        public DateTime Date { get; set; }
        public string CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public ApplicationUser User { get; set; }
        public bool IsSettled { get; set; }
        public boo IsDeleted { get; set; }

    }
}
