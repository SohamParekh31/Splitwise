using Microsoft.AspNetCore.Mvc;
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
            var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            return Ok(_expenseRepository.GetExpense(userId));
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetExpenseBasedOnId(int id)
        {
            return Ok(_expenseRepository.GetExpenseBasedOnId(id));
        }
        [HttpPost]
        public IActionResult PostExpense(AddExpense addExpense)
        {
            return Ok(_expenseRepository.AddExpense(addExpense));
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
            return Ok(_expenseRepository.DeleteExpense(id));
        }
        [HttpPost]
        [Route("settlement")]
        public IActionResult Settlment(SettleUp settleUp)
        {
            return Ok(_expenseRepository.Settlement(settleUp));
        }

    }
}
