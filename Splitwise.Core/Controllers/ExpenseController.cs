using Microsoft.AspNetCore.Mvc;
using Splitwise.DomainModel.Models.ApplicationClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Splitwise.Core.Controllers
{
    [Route("api/Expenses")]
    [ApiController]
    public class ExpenseController : Controller
    {
        public ExpenseController()
        {

        }
        [HttpGet]
        public void Getexpense()
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        [Route("{id}")]
        public void GetExpenseBasedOnId(string id)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public void PostExpense(AddExpense addExpense)
        {
            throw new NotImplementedException();
        }
        [HttpPut]
        [Route("{id}")]
        public void EditExpense(string id, AddExpense addExpense)
        {
            throw new NotImplementedException();
        }
        [HttpDelete]
        [Route("{id}")]
        public void DeleteExpense(string id)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        [Route("settlement")]
        public void Settlment(SettleUp settleUp)
        {
            throw new NotImplementedException();
        }

    }
}
