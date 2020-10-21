using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.RegularExpressions;

namespace Splitwise.DomainModel.Models
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser user { get; set; }

        public DateTime Date { get; set; }
        public int GroupId { get; set; }
        [ForeignKey("GroupId")]
        public Group group { get; set; }
    }
}
