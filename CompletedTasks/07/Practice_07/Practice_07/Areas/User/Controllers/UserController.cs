using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Practice_07.Areas.User.Controllers
{
    [Area("User")]
    [Route("user")]
    public class UserController : Controller
    {
        [Route("")]
        public IActionResult Home()
        {
            return View();
        }
        [Route("info")]
        public IActionResult Info()
        {
            return View();
        }
    }
}
