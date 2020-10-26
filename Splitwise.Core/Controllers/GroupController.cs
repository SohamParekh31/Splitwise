using Microsoft.AspNetCore.Mvc;
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
            return Ok(_groupRepository.GetGroup(userId));
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetGroupBasedOnId(int id)
        {
            return Ok(_groupRepository.GetGroupById(id));
        }
        [HttpPost]
        public IActionResult PostGroup(AddGroup addGroup)
        {
            return Ok(_groupRepository.AddGroup(addGroup));
        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult EditGroup(int id, GroupDetails editGroup)
        {
            return Ok(_groupRepository.EditGroup(id, editGroup));
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteGroup(int id)
        {
            return Ok(_groupRepository.DeleteGroup(id));
        }
        [HttpGet]
        [Route("expense/{id}")]
        public IActionResult GetGroupExpenseList(int id)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            return Ok(_groupRepository.GetGroupExpenseList(id,userId));
        }
    }
    
}
