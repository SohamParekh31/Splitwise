using Microsoft.AspNetCore.Mvc;
using Splitwise.DomainModel.Models;
using Splitwise.DomainModel.Models.ApplicationClasses;
using Splitwise.Repository.GroupRepository;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.Core.Controllers
{
    [Route("api/Groups")]
    [ApiController]
    public class GroupController : Controller
    {
        private readonly IGroupRepository _groupRepository;

        public GroupController(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }
        [HttpGet]
        public IActionResult GroupList()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            var group = _groupRepository.GetGroup(userId);
            return Ok(group);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetGroupBasedOnId(int id)
        {
            var group = _groupRepository.GetGroupById(id);
            return Ok(group);
        }
        [HttpPost]
        public IActionResult PostGroup(AddGroup addGroup)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;
            Group group = _groupRepository.AddGroup(addGroup);
            _groupRepository.AddGroupMember(addGroup, userId, group);
            return Ok();
        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult EditGroup(int id, AddGroup editGroup)
        {
            _groupRepository.EditGroup(id, editGroup);
            return Ok();
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteGroup(int id)
        {
            _groupRepository.DeleteGroup(id);
            return Ok();
        }
        [HttpGet]
        [Route("expense/{id}")]
        public IActionResult GetGroupExpenseList(int id)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            var groupExpense = _groupRepository.GetGroupExpenseList(id, userId);
            return Ok(groupExpense);
        }
    }
    
}
