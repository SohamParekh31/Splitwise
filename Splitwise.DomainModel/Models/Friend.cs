using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Splitwise.DomainModel.Models
{
    public class Friend
    {
        public int FriendId { get; set; }
        public string Friend1 { get; set; }
        [ForeignKey("Friend1")]
        public ApplicationUser User1 { get; set; }
        public string Friend2 { get; set; }
        [ForeignKey("Friend2")]
        public ApplicationUser User2 { get; set; }
    }
}
