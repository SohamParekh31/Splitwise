using Microsoft.AspNetCore.Identity;
using Splitwise.Core.ApplicationClasses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.Repository.AccountRepository
{
    public interface IAccountRepository
    {
        Task<string> Login(Login login);
        Task<IdentityResult> Register(Register register);
    }
}
