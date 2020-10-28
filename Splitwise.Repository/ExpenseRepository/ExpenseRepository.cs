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
            _appDbContext.Expenses.Add(expense);
            _appDbContext.SaveChanges();
            return expense;
        }

        public void AddExpenseInfo(AddExpense addExpense, Expense expense)
        {
            foreach (var item in addExpense.Email)
            {
                foreach (var item1 in addExpense.PaidBy)
                {
                    if (item == item1.Email)
                    {
                        var user = _userManager.FindByEmailAsync(item).Result;
                        ExpenseInfo expenseInfo = new ExpenseInfo();
                        expenseInfo.ExpenseId = expense.ExpenseId;
                        expenseInfo.Paid_Amount = item1.Paid_Amount;
                        expenseInfo.Borrow_Amount = 0;
                        expenseInfo.UserId = user.Id;
                        expenseInfo.Share_Amount = addExpense.Amount / addExpense.Email.Count;
                        expenseInfo.Lent_Amount = expenseInfo.Paid_Amount - expenseInfo.Share_Amount;
                        _appDbContext.ExpenseInfos.Add(expenseInfo);
                    }
                    else
                    {
                        var user = _userManager.FindByEmailAsync(item).Result;
                        ExpenseInfo expenseInfo = new ExpenseInfo();
                        expenseInfo.ExpenseId = expense.ExpenseId;
                        expenseInfo.Paid_Amount = 0;
                        expenseInfo.Borrow_Amount = addExpense.Amount / addExpense.Email.Count;
                        expenseInfo.UserId = user.Id;
                        expenseInfo.Share_Amount = addExpense.Amount / addExpense.Email.Count;
                        expenseInfo.Lent_Amount = 0;
                        _appDbContext.ExpenseInfos.Add(expenseInfo);
                    }
                }
            }
            _appDbContext.SaveChanges();
        }

        public void DeleteExpense(int id)
        {
            var expense = _appDbContext.Expenses.FirstOrDefault(x => x.ExpenseId == id);
            var expenseInfo = _appDbContext.ExpenseInfos.Where(x => x.ExpenseId == id);
            _appDbContext.ExpenseInfos.RemoveRange(expenseInfo);
            _appDbContext.Expenses.Remove(expense);
            _appDbContext.SaveChanges();
        }

        public Task EditExpense(int id, AddExpense editExpense)
        {
            throw new NotImplementedException();
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

        public Task Settlement(SettleUp settleUp)
        {
            throw new NotImplementedException();
        }
    }
}
