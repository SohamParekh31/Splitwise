﻿using Splitwise.DomainModel.Models;
using Splitwise.DomainModel.Models.ApplicationClasses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.Repository.ExpenseRepository
{
    public interface IExpenseRepository
    {
        List<Expense> GetExpense(string id);
        AddExpense GetExpenseBasedOnId(int id);
        Expense AddExpense(AddExpense addExpense);
        void AddExpenseInfo(AddExpense addExpense, Expense expense);
        void EditExpense(int id, AddExpense editExpense);
        void DeleteExpense(int id);
        void SettlementExpense(SettleUp settleUp,string email);
    }
}
