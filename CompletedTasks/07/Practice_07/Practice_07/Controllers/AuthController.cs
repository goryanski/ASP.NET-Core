using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Practice_07.Services.Interfaces;

namespace Practice_07.Controllers
{
    public class AuthController : Controller
    {
        IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string login, string password)
        {
            // TODO: authService.CheckUser(login, password);
            return RedirectToAction("");
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(object obj)
        {
            // TODO: validation
            // TODO: authService.addUser(obj);
            return RedirectToAction("");
        }
    }
}
