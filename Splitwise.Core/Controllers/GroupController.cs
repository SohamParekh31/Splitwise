using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
        public ActionResult<List<GroupReturn>> GroupList()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            var group = _groupRepository.GetGroup(userId);
            return Ok(group);
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult<AddGroup> GetGroupBasedOnId(int id)
        {
            var group = _groupRepository.GetGroupById(id);
            return Ok(group);
        }
        [HttpPost]
        public ActionResult<Group> PostGroup(AddGroup addGroup)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;
            Group group = _groupRepository.AddGroup(addGroup);
            _groupRepository.AddGroupMember(addGroup, userId, group);
            return Ok();
        }
        [HttpPut]
        [Route("{id}")]
        public ActionResult<Group> EditGroup(int id, AddGroup editGroup)
        {
            _groupRepository.EditGroup(id, editGroup);
            return Ok();
        }
        [HttpDelete]
        [Route("{id}")]
        public ActionResult<Group> DeleteGroup(int id)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;
            _groupRepository.DeleteGroup(id,userId);
            return Ok();
        }
        [HttpGet]
        [Route("expense/{id}")]
        public ActionResult<List<Expense>> GetGroupExpenseList(int id)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            var groupExpense = _groupRepository.GetGroupExpenseList(id, userId);
            return Ok(groupExpense);
        }
    }
    
}
