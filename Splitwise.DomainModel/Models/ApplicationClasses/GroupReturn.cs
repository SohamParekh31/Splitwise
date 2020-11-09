using System;
using System.Collections.Generic;
using System.Text;

namespace Splitwise.DomainModel.Models.ApplicationClasses
{
    public class GroupReturn
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string createdBy { get; set; }
    }
}
