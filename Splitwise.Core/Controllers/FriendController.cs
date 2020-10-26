using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Splitwise.DomainModel.Models;
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
        private readonly UserManager<ApplicationUser> userManager;

        public FriendController(IFriendRepository friendRepository,UserManager<ApplicationUser> userManager)
        {
            _friendRepository = friendRepository;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetFriendList()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;
            var user = userManager.FindByEmailAsync(userId).Result;
            return Ok(_friendRepository.GetFriendsList(user.Id));
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
            return Ok(_friendRepository.DeleteFriend(id,userId));
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
