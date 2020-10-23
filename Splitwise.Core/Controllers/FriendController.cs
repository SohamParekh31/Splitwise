using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Splitwise.DomainModel.Models.ApplicationClasses;
using Splitwise.Repository.FriendRepository;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Splitwise.Core.Controllers
{
    [AllowAnonymous]
    [Route("api/Friends")]
    [ApiController]
    public class FriendController : Controller
    {
        private readonly IFriendRepository _friendRepository;

        public FriendController(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }

        [HttpGet]
        public IActionResult GetFriendList()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            return Ok(_friendRepository.GetFriendsList(userId));
        }
        [HttpPost]
        public IActionResult PostFriend(AddFriend addFriend)
        {
            return Ok(_friendRepository.AddFriend(addFriend));
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteFriend(string id)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            return Ok(_friendRepository.DeleteFriend(userId));
        }
        [HttpGet]
        [Route("expense/{id}")]
        public IActionResult GetFriendExpenseList(string id)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            return Ok(_friendRepository.GetFriendsExpenseList(id,userId));
        }
    }
}
