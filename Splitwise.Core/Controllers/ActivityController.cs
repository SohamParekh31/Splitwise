using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Splitwise.DomainModel.Models;
using Splitwise.Repository.ActivityRepository;

namespace Splitwise.Core.Controllers
{
    [Route("api/Activities")]
    [ApiController]
    public class ActivityController : Controller
    {
        private readonly IActivityRepository _activityRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ActivityController(IActivityRepository activityRepository,UserManager<ApplicationUser> userManager)
        {
            _activityRepository = activityRepository;
            _userManager = userManager;
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<Activity>> ActivityList()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;
            var activity = _activityRepository.ActivityList(userId);
            return Ok(activity);
        }
    }
}
