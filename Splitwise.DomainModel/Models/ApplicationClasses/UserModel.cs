using System;
using System.Collections.Generic;
using System.Text;

namespace Splitwise.DomainModel.Models.ApplicationClasses
{
    public class UserModel
    {
        public ApplicationUser User { get; set; }

        public List<Expense> Expenses { get; set; }

        public List<GroupReturn> Groups { get; set; }

        public List<PayerModel> Owesfrom { get; set; }

        public List<PayerModel> Owsto { get; set; }

        public List<Activity> Activities { get; set; }

        public List<PaymentBook> Transactions { get; set; }
    }
}
