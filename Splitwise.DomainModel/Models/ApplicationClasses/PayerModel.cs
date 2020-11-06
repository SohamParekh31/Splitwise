using System;
using System.Collections.Generic;
using System.Text;

namespace Splitwise.DomainModel.Models.ApplicationClasses
{
    public class PayerModel
    {
        public string PayerId { get; set; }
        public ApplicationUser Payer { get; set; }
        public double Amount { get; set; }
    }
}
