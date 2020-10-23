using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Splitwise.Repository.ActivityRepository;

namespace Splitwise.Core.Controllers
{
    [Route("api/Activities")]
    [ApiController]
    public class ActivityController : Controller
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityController(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult ActivityList()
        {
            return Ok(_activityRepository.ActivityList());
        }
    }
}
