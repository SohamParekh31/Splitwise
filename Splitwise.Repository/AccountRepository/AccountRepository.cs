using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Splitwise.Core.ApplicationClasses;
using Splitwise.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.Repository.AccountRepository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;

        public AccountRepository(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }
        public async Task<string> Login(Login login)
        {
            var loggedInUser = await userManager.FindByEmailAsync(login.Email);
            if(loggedInUser != null)
            {
                var result = await signInManager.PasswordSignInAsync(loggedInUser.UserName, login.Password, login.RememberMe, false);
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
                        expires: DateTime.Now.AddHours(3),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );
                    var returntoken = new JwtSecurityTokenHandler().WriteToken(token);
                    return returntoken;
                };
            }
            return null;
        }
    }
}
