using System;
using System.Collections.Generic;
using System.Text;

namespace Splitwise.DomainModel.Models.ApplicationClasses
{
    public class Share
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Share_Percentage { get; set; }
        public float Share_Amount { get; set; }
    }
}
