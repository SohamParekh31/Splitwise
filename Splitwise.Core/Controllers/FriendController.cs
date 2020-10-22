using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Splitwise.DomainModel.Models.ApplicationClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Splitwise.Core.Controllers
{
    [AllowAnonymous]
    [Route("api/Friends")]
    [ApiController]
    public class FriendController : Controller
    {

        [HttpGet]
        public void GetFriendList()
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public void PostFriend(AddFriend addFriend)
        {
            throw new NotImplementedException();
        }
        [HttpDelete]
        [Route("{id}")]
        public void DeleteFriend(string id)
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        [Route("expense/{id}")]
        public void GetFriendExpenseList(string id)
        {
            throw new NotImplementedException();
        }
    }
}
