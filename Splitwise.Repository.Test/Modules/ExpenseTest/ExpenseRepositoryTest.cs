using Moq;
using Xunit;
using System;
using Splitwise.DomainModel.Models.ApplicationClasses;

namespace Splitwise.Repository.Test.Modules.ExpenseTest
{
    public class ExpenseRepositoryTest
    {
        ExpenseRepository.ExpenseRepository ep = new ExpenseRepository.ExpenseRepository();
        [Fact]
        public void AddExpenseTest()
        {
            AddExpense addExpense = new AddExpense();
            Assert.ThrowsAsync<NotImplementedException>(() => ep.AddExpense(addExpense));
        }
        [Fact]
        public void DeleteExpenseTest()
        {
            var id = 1;
            Assert.ThrowsAsync<NotImplementedException>(() => ep.DeleteExpense(id));
        }
        [Fact]
        public void EditExpenseTest()
        {
            var id = 1;
            AddExpense editExpense = new AddExpense();
            Assert.ThrowsAsync<NotImplementedException>(() => ep.EditExpense(id,editExpense));
        }
        [Fact]
        public void GetExpenseTest()
        {
            var userId = "82467ddb-6ff7-46ee-80b0-3b306c63b430";
            Assert.ThrowsAsync<NotImplementedException>(() => ep.GetExpense(userId));
        }
        [Fact]
        public void GetExpenseBasedOnIDTest()
        {
            var id = 1;
            Assert.ThrowsAsync<NotImplementedException>(() => ep.GetExpenseBasedOnId(id));
        }
        [Fact]
        public void SettlementTest()
        {
            SettleUp settleUp = new SettleUp();
            Assert.ThrowsAsync<NotImplementedException>(() => ep.Settlement(settleUp));
        }
    }
}
