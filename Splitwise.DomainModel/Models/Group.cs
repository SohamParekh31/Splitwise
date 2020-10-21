using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Splitwise.DomainModel.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public ApplicationUser User { get; set; }
        public DateTime Date { get; set; }
        public bool SimplyfyDebits { get; set; }
        public bool IsDeleted { get; set; }
    }
}
