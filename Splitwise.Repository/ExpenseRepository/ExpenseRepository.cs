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

        public Expense GetExpenseBasedOnId(int id)
        {
            var expense = _appDbContext.Expenses.FirstOrDefault(x => x.ExpenseId == id);
            var user = _userManager.FindByIdAsync(expense.CreatedBy).Result;
            var group = _appDbContext.Groups.FirstOrDefault(x => x.GroupId == expense.GroupId);
            expense.User = user;
            expense.Group = group;
            return expense;
        }

        public PaymentBook SettlementExpense(SettleUp settleUp)
        {
            Settlement settlement;
            if (settleUp.GroupId != null)
                settlement = _appDbContext.Settlements.FirstOrDefault(x => x.Payee == settleUp.Payee && x.Payer == settleUp.Payer && x.GroupId == settleUp.GroupId);
            else
                settlement = _appDbContext.Settlements.FirstOrDefault(x => x.Payee == settleUp.Payee && x.Payer == settleUp.Payer);
            PaymentBook paymentBook = new PaymentBook()
            {
                Payer = settleUp.Payer,
                Payee = settleUp.Payee,
                Paid_Amount = settleUp.Amount,
                SettlementId = settlement.SettlementId
            };
            _appDbContext.PaymentBooks.Add(paymentBook);
            settlement.Amount -= settleUp.Amount;
            _appDbContext.Settlements.Update(settlement);
            _appDbContext.SaveChanges();
            return paymentBook;
        }
    }
}
