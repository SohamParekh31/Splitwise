using Splitwise.DomainModel.Models.ApplicationClasses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.Repository.ExpenseRepository
{
    public class ExpenseRepository : IExpenseRepository
    {
        public Task AddExpense(AddExpense addExpense)
        {
            throw new NotImplementedException();
        }

        public Task DeleteExpense(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditExpense(int id, AddExpense editExpense)
        {
            throw new NotImplementedException();
        }

        public Task GetExpense(string id)
        {
            throw new NotImplementedException();
        }

        public Task GetExpenseBasedOnId(int id)
        {
            throw new NotImplementedException();
        }

        public Task Settlement(SettleUp settleUp)
        {
            throw new NotImplementedException();
        }
    }
}
