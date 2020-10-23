using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Splitwise.Core.ApplicationClasses;
using Splitwise.Repository.AccountRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.Core.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(Login login)
        {
            var loginReturnModel = await _accountRepository.Login(login);
            if (loginReturnModel != null)
            {
                return Ok(loginReturnModel);
            }
            else
            {
                return BadRequest(new { message = "Username or password is incorrect." });
            }
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(Register register)
        {
            var RegisteReturnModel = await _accountRepository.Register(register);
            if (RegisteReturnModel != null)
            {
                return Ok(RegisteReturnModel);
            }
            else
            {
                return BadRequest(new { message = "User with Same Email Exist!!" });
            }
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("logout")]
        public void Logout()
        {
            throw new NotImplementedException();
        }
    }
}
