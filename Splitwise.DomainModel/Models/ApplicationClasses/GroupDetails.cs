using System;
using System.Collections.Generic;
using System.Text;

namespace Splitwise.DomainModel.Models.ApplicationClasses
{
    public class GroupDetails
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Date { get; set; }
        public bool SimplyfyDebits { get; set; }
        public bool IsDeleted { get; set; }
        public List<GroupUserMapping> Users { get; set; }
    }
}
