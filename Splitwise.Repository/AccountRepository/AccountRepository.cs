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
                        IActivityRepository activityRepository, AppDbContext appDbContext)
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
            if (loggedInUser != null)
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
            if (checkUser == null)
            {
                return await userManager.CreateAsync(user, register.Password);
            }
            return null;
        }

        public UserModel GetUserInfo(string userId)
        {
            List<PayerModel> Owsfrom = new List<PayerModel>();
            List<PayerModel> Owsto = new List<PayerModel>();
            UserModel userModel = new UserModel();
            userModel.User = userManager.FindByEmailAsync(userId).Result;
            userModel.Expenses = expenseRepository.GetExpense(userId);
            userModel.Groups = groupRepository.GetGroup(userId);
            userModel.Activities = activityRepository.ActivityList(userId);
            var appUser = userManager.FindByEmailAsync(userId).Result;
            var settlements = appDbContext.Settlements.Where(x => x.Payee == appUser.Id || x.Payer == appUser.Id).ToList();
            var owesToGroup = (from item in settlements
                               where item.Payer == appUser.Id
                               group item by item.Payee into Owesto
                               orderby Owesto.Key
                               select new
                               {
                                   name = Owesto.Key,
                                   amount = Owesto.Sum(x => x.Amount)
                               }).ToList();
            var owesFromGroup = (from item in settlements
                                 where item.Payee == appUser.Id
                                 group item by item.Payer into Owesfrom
                                 orderby Owesfrom.Key
                                 select new
                                 {
                                     name = Owesfrom.Key,
                                     amount = Owesfrom.Sum(x => x.Amount)
                                 }).ToList();
            foreach (var item in owesFromGroup)
            {
                var user = userManager.FindByIdAsync(item.name).Result;

                
                if (owesToGroup.Count != 0)
                {
                    PayerModel payerModel = new PayerModel()
                    {
                        Amount = 0,
                        Payer = user,
                        PayerId = user.Id
                    };
                    foreach (var subitem in owesToGroup)
                    {
                        var result = item.amount - subitem.amount;
                        if (item.name == subitem.name)
                        {
                            if (result > 0)
                            {

                                payerModel.Amount = result;
                                Owsfrom.Add(payerModel);

                            }
                            else if (result < 0)
                            {

                                payerModel.Amount = result * -1;
                                Owsto.Add(payerModel);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            PayerModel payerModel1 = new PayerModel
                            {
                                Amount = item.amount,
                                PayerId = item.name,
                                Payer = user
                            };
                            Owsfrom.Add(payerModel1);
                        }
                    }
                }
                else
                {
                    PayerModel payerModel = new PayerModel()
                    {
                        Amount = item.amount,
                        Payer = user,
                        PayerId = user.Id
                    };
                    Owsfrom.Add(payerModel);
                }
            }
            userModel.Owesfrom = Owsfrom.ToList();
            userModel.Owsto = Owsto.ToList();
            return userModel;
        }
    }
}
