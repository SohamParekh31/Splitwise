using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Splitwise.DomainModel.Models
{
    public class ApplicationUser : IdentityUser
    {
        public float Balance { get; set; }
    }
}
