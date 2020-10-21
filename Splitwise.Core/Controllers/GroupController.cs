using Microsoft.AspNetCore.Mvc;
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
        public string Index()
        {
            return "Hello SP";
        }
    }
    
}
