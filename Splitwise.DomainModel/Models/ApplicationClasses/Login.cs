using System;
using System.Collections.Generic;
using System.Text;

namespace Splitwise.Core.ApplicationClasses
{
    public class Login
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
