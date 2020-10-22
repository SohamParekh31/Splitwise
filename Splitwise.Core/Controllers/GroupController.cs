using Microsoft.AspNetCore.Mvc;
using Splitwise.DomainModel.Models.ApplicationClasses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Splitwise.Core.Controllers
{
    [Route("api/Groups")]
    [ApiController]
    public class GroupController : Controller
    {
        public GroupController()
        {

        }
        [HttpGet]
        public void GroupList()
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        [Route("{id}")]
        public void GetGroupBasedOnId(string id)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public void PostGroup(AddGroup addGroup)
        {
            throw new NotImplementedException();
        }
        [HttpPut]
        [Route("{id}")]
        public void EditGroup(string id,AddGroup addGroup)
        {
            throw new NotImplementedException();
        }
        [HttpDelete]
        [Route("{id}")]
        public void DeleteGroup(string id)
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        [Route("expense/{id}")]
        public void GetGroupExpenseList(string id)
        {
            throw new NotImplementedException();
        }
    }
    
}
