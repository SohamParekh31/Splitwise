using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Splitwise.DomainModel.Data;
using Splitwise.DomainModel.Models;
using Splitwise.DomainModel.Models.ApplicationClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.Repository.ExpenseRepository
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public ExpenseRepository(AppDbContext appDbContext, UserManager<ApplicationUser> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }
        public Expense AddExpense(AddExpense addExpense)
        {
            Expense expense = new Expense()
            {
                Description = addExpense.Description,
                Amount = addExpense.Amount,
                SplitType = addExpense.SplitType,
                GroupId = addExpense.GroupId,
                Date = addExpense.Date,
                CreatedBy = addExpense.CreatedBy,
                IsSettled = addExpense.IsSettled,
                IsDeleted = addExpense.IsDeleted
            };
            var group = _appDbContext.Groups.FirstOrDefault(x => x.GroupId == addExpense.GroupId);
            Activity activity = new Activity()
            {
                Description = "You Added Expense "+ addExpense.Description + " in "+ group.Name,
                UserId = addExpense.CreatedBy,
                Date = addExpense.Date,
            };
            _appDbContext.Activities.Add(activity);
            foreach (var item in addExpense.Shares)
            {
                var user = _userManager.FindByEmailAsync(item.Email).Result;
                if (user.Id != addExpense.CreatedBy)
                {
                    Activity activity1 = new Activity()
                    {
                        Description = "You were Added in Expense " + addExpense.Description + " in " + group.Name,
                        UserId = user.Id,
                        Date = addExpense.Date,
                    };
                    _appDbContext.Activities.Add(activity1);
                }
            }
            _appDbContext.Expenses.Add(expense);
            _appDbContext.SaveChanges();
            return expense;
        }

        public void AddExpenseInfo(AddExpense addExpense, Expense expense)
        {
            float exp = 0;
            if(addExpense.SplitType == "Percentage")
            {
                foreach (var item in addExpense.Shares)
                {
                    foreach (var item1 in addExpense.PaidBy)
                    {
                        if(item.Email != item1.Email)
                            exp += (item.Share_Percentage * addExpense.Amount) / 100;
                    }
                    
                }
            }
            
            foreach (var item in addExpense.Shares)
            {
                foreach (var item1 in addExpense.PaidBy)
                {
                    var user = _userManager.FindByEmailAsync(item.Email).Result;
                    var userItem1 = _userManager.FindByEmailAsync(item1.Email).Result;
                    if (addExpense.SplitType == "Percentage")
                    {
                        if (item.Email == item1.Email)
                        {
                            ExpenseInfo expenseInfo = new ExpenseInfo();
                            expenseInfo.ExpenseId = expense.ExpenseId;
                            expenseInfo.Paid_Amount = item1.Paid_Amount;
                            expenseInfo.Borrow_Amount = 0;
                            expenseInfo.UserId = user.Id;
                            expenseInfo.Share_Amount = expenseInfo.Paid_Amount - exp;
                            expenseInfo.Lent_Amount = exp;
                            _appDbContext.ExpenseInfos.Add(expenseInfo);
                        }
                        else
                        {
                            ExpenseInfo expenseInfo = new ExpenseInfo();
                            expenseInfo.ExpenseId = expense.ExpenseId;
                            expenseInfo.Paid_Amount = 0;
                            expenseInfo.Borrow_Amount = (item.Share_Percentage * addExpense.Amount) / 100;
                            expenseInfo.UserId = user.Id;
                            expenseInfo.Share_Amount = (item.Share_Percentage * addExpense.Amount) / 100;
                            expenseInfo.Lent_Amount = 0;
                            _appDbContext.ExpenseInfos.Add(expenseInfo);
                        }
                        Settlement settlement = new Settlement();
                        settlement.ExpenseId = expense.ExpenseId;
                        settlement.Payer = user.Id;
                        settlement.Payee = userItem1.Id;
                        settlement.Date = addExpense.Date;
                        settlement.Amount = (item.Share_Percentage * addExpense.Amount) / 100;
                        if (addExpense.GroupId != null)
                            settlement.GroupId = addExpense.GroupId;
                        if (settlement.Payer != settlement.Payee)
                            _appDbContext.Settlements.Add(settlement);
                    }
                    else
                    {
                        if (item.Email == item1.Email)
                        {
                            ExpenseInfo expenseInfo = new ExpenseInfo();
                            expenseInfo.ExpenseId = expense.ExpenseId;
                            expenseInfo.Paid_Amount = item1.Paid_Amount;
                            expenseInfo.Borrow_Amount = 0;
                            expenseInfo.UserId = user.Id;
                            expenseInfo.Share_Amount = addExpense.Amount / addExpense.Shares.Count;
                            expenseInfo.Lent_Amount = expenseInfo.Paid_Amount - expenseInfo.Share_Amount;
                            _appDbContext.ExpenseInfos.Add(expenseInfo);
                        }
                        else
                        {
                            ExpenseInfo expenseInfo = new ExpenseInfo();
                            expenseInfo.ExpenseId = expense.ExpenseId;
                            expenseInfo.Paid_Amount = 0;
                            expenseInfo.Borrow_Amount = addExpense.Amount / addExpense.Shares.Count;
                            expenseInfo.UserId = user.Id;
                            expenseInfo.Share_Amount = addExpense.Amount / addExpense.Shares.Count;
                            expenseInfo.Lent_Amount = 0;
                            _appDbContext.ExpenseInfos.Add(expenseInfo);
                        }
                        Settlement settlement = new Settlement();
                        settlement.ExpenseId = expense.ExpenseId;
                        settlement.Payer = user.Id;
                        settlement.Payee = userItem1.Id;
                        settlement.Date = addExpense.Date;
                        settlement.Amount = addExpense.Amount / addExpense.Shares.Count;
                        if (addExpense.GroupId != null)
                            settlement.GroupId = addExpense.GroupId;
                        if(settlement.Payer != settlement.Payee)
                            _appDbContext.Settlements.Add(settlement);
                    } 
                    
                }
            }
            _appDbContext.SaveChanges();
        }
        public void DeleteExpense(int id)
        {
            var settlement = _appDbContext.Settlements.Where(x => x.ExpenseId == id);
            _appDbContext.Settlements.RemoveRange(settlement);
            var expense = _appDbContext.Expenses.FirstOrDefault(x => x.ExpenseId == id);
            var expenseInfo = _appDbContext.ExpenseInfos.Where(x => x.ExpenseId == id);
            _appDbContext.ExpenseInfos.RemoveRange(expenseInfo);
            _appDbContext.Expenses.Remove(expense);
            _appDbContext.SaveChanges();
        }

        public void EditExpense(int id, AddExpense editExpense)
        {
            var expense = _appDbContext.Expenses.FirstOrDefault(x => x.ExpenseId == id);
            var expenseInfos = _appDbContext.ExpenseInfos.Where(x => x.ExpenseId == id);
            _appDbContext.ExpenseInfos.RemoveRange(expenseInfos);
            var settlements = _appDbContext.Settlements.Where(x => x.ExpenseId == id);
            _appDbContext.Settlements.RemoveRange(settlements);
            expense.ExpenseId = id;
            expense.Description = editExpense.Description;
            expense.Amount = editExpense.Amount;
            expense.SplitType = editExpense.SplitType;
            expense.GroupId = editExpense.GroupId;
            expense.Date = editExpense.Date;
            expense.CreatedBy = editExpense.CreatedBy;
            expense.IsSettled = editExpense.IsSettled;
            expense.IsDeleted = editExpense.IsDeleted;
            _appDbContext.Expenses.Update(expense);
            _appDbContext.SaveChanges();
            AddExpenseInfo(editExpense, expense);
        }

        public List<Expense> GetExpense(string id)
        {
            var user = _userManager.FindByEmailAsync(id).Result;
            var expenseList = _appDbContext.Expenses.Where(x => x.CreatedBy == user.Id).Include(e => e.User).Include(e => e.Group);
            return expenseList.ToList();
        }

        public AddExpense GetExpenseBasedOnId(int id)
        {
            var expense = _appDbContext.Expenses.FirstOrDefault(x => x.ExpenseId == id);
            var user = _userManager.FindByIdAsync(expense.CreatedBy).Result;
            var group = _appDbContext.Groups.FirstOrDefault(x => x.GroupId == expense.GroupId);
            var expenseInfo = _appDbContext.ExpenseInfos.Where(x => x.ExpenseId == id).ToList();
            AddExpense getExpense = new AddExpense();
            getExpense.ExpenseId = expense.ExpenseId;
            getExpense.Description = expense.Description;
            getExpense.Amount = expense.Amount;
            getExpense.SplitType = expense.SplitType;
            getExpense.GroupId = expense.GroupId;
            getExpense.Date = expense.Date;
            getExpense.CreatedBy = expense.CreatedBy;
            getExpense.IsDeleted = expense.IsDeleted;
            getExpense.IsSettled = expense.IsSettled;
            getExpense.user = user;
            getExpense.Group = group;
            getExpense.Shares = new List<Share>();
            getExpense.PaidBy = new List<UserExpenseMapper>();
            foreach (var item in expenseInfo)
            {
                var appUser = _userManager.FindByIdAsync(item.UserId).Result;
                if(item.Paid_Amount != 0)
                {
                    UserExpenseMapper userExpenseMapper = new UserExpenseMapper()
                    {
                        Name = appUser.FirstName,
                        Email = appUser.Email,
                        Paid_Amount = item.Paid_Amount
                    };
                    getExpense.PaidBy.Add(userExpenseMapper);
                }
                else
                {
                    Share share = new Share
                    {
                        Name = appUser.FirstName,
                        Email = appUser.Email
                    };
                    getExpense.Shares.Add(share);
                }
            }
            return getExpense;
        }

        public void SettlementExpense(SettleUp settleUp,string email)
        {
            var user = _userManager.FindByEmailAsync(email).Result;
            var payerDetails = _userManager.FindByEmailAsync(settleUp.Payer).Result;
            var payeeDetails = _userManager.FindByEmailAsync(settleUp.Payee).Result;
            var settlement = _appDbContext.Settlements.Where(x => x.Payee == payeeDetails.Id && x.Payer == payerDetails.Id).ToList();
            var settlementReverse = _appDbContext.Settlements.Where(x => x.Payee == payerDetails.Id && x.Payer == payeeDetails.Id).ToList();
            foreach (var item in settlement)
            {
                var settlementSum = settlement.Sum(x => x.Amount);
                if(settlementReverse.Count != 0)
                {
                    settlementSum = settlement.Sum(x => x.Amount) - settlementReverse.Sum(x => x.Amount);
                    if(settlementSum > 0)
                    {
                        foreach (var settlementreverse in settlementReverse)
                        {
                            settlementreverse.Amount = 0;
                            _appDbContext.Settlements.Update(settlementreverse);

                            _appDbContext.SaveChanges();
                        }
                    }
                }
                if (item.Amount != 0 && settleUp.Amount >= 0)
                {
                    PaymentBook paymentBook = new PaymentBook()
                    {
                        Payer = payerDetails.Id,
                        Payee = payeeDetails.Id,
                        Paid_Amount = settleUp.Amount,
                    };
                    _appDbContext.PaymentBooks.Add(paymentBook);
                    if (payeeDetails.Id == user.Id)
                    {
                        Activity activity = new Activity()
                        {
                            Description = "You Received Payment of Rs." + settleUp.Amount + " from " + payerDetails.FirstName,
                            UserId = user.Id,
                            Date = DateTime.Today
                        };
                        _appDbContext.Activities.Add(activity);
                    }
                    else if (payerDetails.Id == user.Id)
                    {
                        Activity activity = new Activity()
                        {
                            Description = "You Paid  Rs." + settleUp.Amount + " to " + payeeDetails.FirstName,
                            UserId = user.Id,
                            Date = DateTime.Today
                        };
                        _appDbContext.Activities.Add(activity);
                    }
                    _appDbContext.SaveChanges();
                    if ((settlementSum - settleUp.Amount) == 0)
                    {
                        foreach (var item1 in settlement)
                        {
                            item1.Amount = 0;
                            _appDbContext.Settlements.Update(item1);
                            _appDbContext.SaveChanges();
                        }
                    }
                    else
                    {
                        foreach (var item1 in settlement)
                        {
                            var result = settleUp.Amount - item1.Amount;
                            settleUp.Amount -= item1.Amount;
                            if(result != 0 && result > item1.Amount)
                            {
                                item1.Amount = 0;
                                _appDbContext.Settlements.Update(item1);
                                _appDbContext.SaveChanges();
                            }
                            else
                            {
                                item1.Amount = result * -1;
                                _appDbContext.Settlements.Update(item1);
                                _appDbContext.SaveChanges();
                            }
                        }
                    }
                }
            }
        }
    }
}
