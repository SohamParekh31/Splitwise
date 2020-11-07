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
    [Authorize]
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
        public ActionResult<List<Friend>> GetFriendList()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;
            var user = userManager.FindByEmailAsync(userId).Result;
            var friendlist = _friendRepository.GetFriendsList(user.Id);
            return Ok(friendlist);
        }
        [HttpPost]
        public ActionResult<Friend> PostFriend(AddFriend addFriend)
        {
            _friendRepository.AddFriend(addFriend);
            return Ok();
        }
        [HttpDelete]
        [Route("{id}")]
        public ActionResult<Group> DeleteFriend(string id)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;
            _friendRepository.DeleteFriend(id, userId);
            return Ok();
        }
        [HttpGet]
        [Route("expense/{id}")]
        public ActionResult<List<Settlement>> GetFriendExpenseList(string id)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;
            var settlement = _friendRepository.GetFriendsExpenseList(id, userId);
            return Ok(settlement);
        }
        [HttpGet]
        [Route("{id}")]
        public ActionResult<UserModel> GetFriendDetail(string id)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;
            var currentUserId = userManager.FindByEmailAsync(userId).Result;
            var user = userManager.FindByIdAsync(id).Result;
            var userModel = _friendRepository.GetFriendDetails(user.Email,currentUserId.Id);
            return Ok(userModel);
        }
    }
}
