using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Splitwise.DomainModel.Models;
using Splitwise.DomainModel.Models.ApplicationClasses;
using Splitwise.Repository.ExpenseRepository;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Splitwise.Core.Controllers
{
    [Authorize]
    [Route("api/Expenses")]
    [ApiController]
    public class ExpenseController : Controller
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseController(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }
        [HttpGet]
        public ActionResult<List<Expense>> Getexpense()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;
            var expense = _expenseRepository.GetExpense(userId);
            return Ok(expense);
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult<Expense> GetExpenseBasedOnId(int id)
        {
            var expense = _expenseRepository.GetExpenseBasedOnId(id);
            return Ok(expense);
        }
        [HttpPost]
        public ActionResult<Expense> PostExpense(AddExpense addExpense)
        {
            Expense expense = _expenseRepository.AddExpense(addExpense);
            _expenseRepository.AddExpenseInfo(addExpense, expense);
            return Ok();
        }
        [HttpPut]
        [Route("{id}")]
        public ActionResult<Expense> EditExpense(int id, AddExpense addExpense)
        {
            _expenseRepository.EditExpense(id, addExpense);
            return Ok();
        }
        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteExpense(int id)
        {
            _expenseRepository.DeleteExpense(id);
            return Ok();
        }
        [HttpPost]
        [Route("settlement")]
        public ActionResult<PaymentBook> Settlment(SettleUp settleUp)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;
            _expenseRepository.SettlementExpense(settleUp,userId);
            return Ok();
        }

    }
}
