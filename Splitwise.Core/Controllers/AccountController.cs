using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Splitwise.Core.ApplicationClasses;
using Splitwise.DomainModel.Models;
using Splitwise.Repository.AccountRepository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.Core.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;

        public AccountController(IAccountRepository accountRepository, UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _accountRepository = accountRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<Login>> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var loggedInUser = await userManager.FindByEmailAsync(login.Email);
                if (loggedInUser != null)
                {
                    var result = await signInManager.PasswordSignInAsync(loggedInUser.UserName, login.Password, login.RememberMe, true);
                    if (result.Succeeded)
                    {

                        var authClaims = new List<Claim>
                        {
                             new Claim("name", loggedInUser.UserName),
                    new Claim(ClaimTypes.Name, loggedInUser.UserName),
                    new Claim(ClaimTypes.Email, loggedInUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                        };


                        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["ApplicationSettings:JWT_Secret"]));

                        var token = new JwtSecurityToken(
                            issuer: configuration["ApplicationSettings:ValidIssuer"],
                            audience: configuration["ApplicationSettings:ValidAudience"],
                            expires: DateTime.Now.AddHours(3),
                            claims: authClaims,
                            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                            );

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                        });
                    }
                }
            }
            return BadRequest(new { message = "Username or password is incorrect." });
            //var loginReturnModel = await _accountRepository.Login(login);
            //if (loginReturnModel != null)
            //{
            //    return Ok(loginReturnModel);
            //}
            //else
            //{
            //    return BadRequest(new { message = "Username or password is incorrect." });
            //}
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public ActionResult<Register> Register(Register register)
        {
            var RegisteReturnModel =  _accountRepository.Register(register);
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
        [HttpGet]
        [Route("logout")]
        public void Logout()
        {
            throw new NotImplementedException();
        }
    }
}
