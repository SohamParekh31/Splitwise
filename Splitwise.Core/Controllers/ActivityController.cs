﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Splitwise.Core.Controllers
{
    [Route("api/Activities")]
    [ApiController]
    public class ActivityController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public void ActivityList()
        {
            throw new NotImplementedException();
        }
    }
}
