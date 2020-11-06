using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Splitwise.Core.ApplicationClasses;
using Splitwise.DomainModel.Data;
using Splitwise.DomainModel.Models;
using Splitwise.DomainModel.Models.ApplicationClasses;
using Splitwise.Repository.ActivityRepository;
using Splitwise.Repository.ExpenseRepository;
using Splitwise.Repository.GroupRepository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
        private readonly IExpenseRepository expenseRepository;
        private readonly IGroupRepository groupRepository;
        private readonly IActivityRepository activityRepository;
        private readonly AppDbContext appDbContext;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
                        IConfiguration configuration, IExpenseRepository expenseRepository, IGroupRepository groupRepository,
                        IActivityRepository activityRepository,AppDbContext appDbContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.expenseRepository = expenseRepository;
            this.groupRepository = groupRepository;
            this.activityRepository = activityRepository;
            this.appDbContext = appDbContext;
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
                            new Claim("ID",loggedInUser.Id),
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

        public async Task<IdentityResult> Register(Register register)
        {
            var user = new ApplicationUser
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                Email = register.Email,
                UserName = register.Email,
                Currency = register.Currency,
                PhoneNumber = register.PhoneNumber,
                Balance = 0
            };
            var checkUser = await userManager.FindByEmailAsync(register.Email);
            if(checkUser == null)
            {
                return await userManager.CreateAsync(user, register.Password);
            }
            return null;
        }

        public UserModel GetUserInfo(string userId)
        {
            List<PayerModel> Owesfrom = new List<PayerModel>();
            List<PayerModel> Owsto = new List<PayerModel>();
            UserModel userModel = new UserModel();
            userModel.User = userManager.FindByEmailAsync(userId).Result;
            userModel.Expenses = expenseRepository.GetExpense(userId);
            userModel.Groups = groupRepository.GetGroup(userId);
            userModel.Activities = activityRepository.ActivityList(userId);
            var appUser = userManager.FindByEmailAsync(userId).Result;
            var settlements = appDbContext.Settlements.Where(x => x.Payee == appUser.Id || x.Payer == appUser.Id).ToList();
            foreach (var item in settlements)
            {
                if(item.Payee == appUser.Id)
                {
                    var user = userManager.FindByIdAsync(item.Payer).Result;
                    PayerModel payerModel = new PayerModel()
                    {
                        Amount = item.Amount,
                        Payer = user,
                        PayerId = user.Id
                    };
                    Owesfrom.Add(payerModel);
                }
                else
                {
                    var user = userManager.FindByIdAsync(item.Payee).Result;
                    PayerModel payerModel = new PayerModel()
                    {
                        Amount = item.Amount,
                        Payer = user,
                        PayerId = user.Id
                    };
                    Owsto.Add(payerModel);
                }
            }
            userModel.Owesfrom = Owesfrom;
            userModel.Owsto = Owsto;
            return userModel;
        }
    }
}
