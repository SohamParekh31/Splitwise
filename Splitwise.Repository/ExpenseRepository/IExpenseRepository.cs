using Splitwise.DomainModel.Models;
using Splitwise.DomainModel.Models.ApplicationClasses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.Repository.ExpenseRepository
{
    public interface IExpenseRepository
    {
        Task<List<Expense>> GetExpense(string id);
        Task GetExpenseBasedOnId(int id);
        Task AddExpense(AddExpense addExpense);
        Task EditExpense(int id, AddExpense editExpense);
        Task DeleteExpense(int id);
        Task Settlement(SettleUp settleUp);
    }
}
