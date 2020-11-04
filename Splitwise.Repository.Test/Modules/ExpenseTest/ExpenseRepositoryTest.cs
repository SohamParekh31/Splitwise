using Moq;
using Xunit;
using System;
using Splitwise.DomainModel.Models.ApplicationClasses;
using Splitwise.Repository.ExpenseRepository;
using System.Collections.Generic;
using Splitwise.DomainModel.Models;

namespace Splitwise.Repository.Test.Modules.ExpenseTest
{
    public class ExpenseRepositoryTest
    {
        //ExpenseRepository.ExpenseRepository ep = new ExpenseRepository.ExpenseRepository();
        //[Fact]
        //public void AddExpenseTest()
        //{
        //    var expenseRepo = new Mock<IExpenseRepository>();
        //    Expense expenses = new Expense() 
        //    {
        //        Description = null,
        //        Amount = 500,
        //        SplitType = "Equal",
        //        GroupId = 1,
        //        Date = new DateTime(2020, 10, 25, 10, 19, 28),
        //        CreatedBy = "82467ddb-6ff7-46ee-80b0-3b306c63b430",
        //        IsDeleted = false,
        //        IsSettled = false
        //    };
        //    AddExpense addExpense = new AddExpense() 
        //    {
        //        Email = new List<string>
        //        {
        //            "sp@gmail.com",
        //            "ayaz@gmail.com"
        //        },
        //        Description = null,
        //        GroupId = 1,
        //        Amount = 500,
        //        SplitType = "Equal",
        //        CreatedBy = "82467ddb-6ff7-46ee-80b0-3b306c63b430",
        //        Date = new DateTime(2020, 10, 25, 10, 19, 28),
        //        IsDeleted = false,
        //        IsSettled = false,
        //        PaidBy = new List<UserExpenseMapper>()
        //        {
        //            new UserExpenseMapper()
        //            {
        //                Email = "sp@gmail.com",
        //                Paid_Amount = 500
        //            }
        //        }
        //    };
        //    expenseRepo.Setup(x => x.AddExpense(addExpense)).Returns(expenses);
        //    var expense = ep.AddExpense(addExpense);
        //    Assert.Equal(expenses, expense);
        //    //Assert.ThrowsAsync<NotImplementedException>(() => ep.AddExpense(addExpense));
        //}
        //[Fact]
        //public void DeleteExpenseTest()
        //{
        //    var expenseRepo = new Mock<IExpenseRepository>();
        //    Expense expenses = new Expense()
        //    {
        //        ExpenseId = 1,
        //        Description = null,
        //        Amount = 500,
        //        SplitType = "Equal",
        //        GroupId = 1,
        //        Date = new DateTime(2020, 10, 25, 10, 19, 28),
        //        CreatedBy = "82467ddb-6ff7-46ee-80b0-3b306c63b430",
        //        IsDeleted = false,
        //        IsSettled = false
        //    };
        //    expenseRepo.Setup(x => x.DeleteExpense(expenses.ExpenseId));
        //    var expense = ep.DeleteExpense(expenses.ExpenseId);
        //    Assert.Null(expense);
        //    //var id = 1;
        //    //Assert.ThrowsAsync<NotImplementedException>(() => ep.DeleteExpense(id));
        //}
        //[Fact]
        //public void EditExpenseTest()
        //{
        //    var id = 1;
        //    AddExpense editExpense = new AddExpense();
        //    Assert.ThrowsAsync<NotImplementedException>(() => ep.EditExpense(id,editExpense));
        //}
        //[Fact]
        //public void GetExpenseTest()
        //{
        //    var userId = "82467ddb-6ff7-46ee-80b0-3b306c63b430";
        //    Assert.ThrowsAsync<NotImplementedException>(() => ep.GetExpense(userId));
        //}
        //[Fact]
        //public void GetExpenseBasedOnIDTest()
        //{
        //    var id = 1;
        //    Assert.ThrowsAsync<NotImplementedException>(() => ep.GetExpenseBasedOnId(id));
        //}
        //[Fact]
        //public void SettlementTest()
        //{
        //    SettleUp settleUp = new SettleUp();
        //    Assert.ThrowsAsync<NotImplementedException>(() => ep.Settlement(settleUp));
        //}
    }
}
