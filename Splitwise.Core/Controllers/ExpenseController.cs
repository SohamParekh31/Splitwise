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
        public IActionResult Getexpense()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;
            var expense = _expenseRepository.GetExpense(userId);
            return Ok(expense);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetExpenseBasedOnId(int id)
        {
            var expense = _expenseRepository.GetExpenseBasedOnId(id);
            return Ok(expense);
        }
        [HttpPost]
        public IActionResult PostExpense(AddExpense addExpense)
        {
            Expense expense = _expenseRepository.AddExpense(addExpense);
            _expenseRepository.AddExpenseInfo(addExpense, expense);
            return Ok();
        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult EditExpense(int id, AddExpense addExpense)
        {
            return Ok(_expenseRepository.EditExpense(id,addExpense));
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteExpense(int id)
        {
            _expenseRepository.DeleteExpense(id);
            return Ok();
        }
        [HttpPost]
        [Route("settlement")]
        public IActionResult Settlment(SettleUp settleUp)
        {
            return Ok(_expenseRepository.SettlementExpense(settleUp));
        }

    }
}
