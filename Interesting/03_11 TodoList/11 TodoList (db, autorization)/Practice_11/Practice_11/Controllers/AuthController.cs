using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice_11.Models.Auth;
using Practice_11.Models.Users.Auth;
using Practice_11.UI.Services.Interfaces;

namespace Practice_11.Controllers
{
    public class AuthController : Controller
    {
        IAuthService authService;
        IUsersService usersService;
        public AuthController(IAuthService authService, IUsersService usersService)
        {
            this.authService = authService;
            this.usersService = usersService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (await authService.UserNotFound(model.Email))
            {
                ModelState.AddModelError("Email", "User Not Found");
                return View(model);
            }
            else
            {
                if (await authService.IncorrectUserPassword(model.Email, model.Password))
                {
                    ModelState.AddModelError("Password", "Incorrect User Password");
                }

                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                
                await authService.Authentication(model.Email, HttpContext);
                return RedirectToAction("Index", "Home"); 
            }
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegisterViewModel model)
        {

            if (!await authService.IsUserEmailUnique(model.Email))
            {
                ModelState.AddModelError("Email", "Your email is not unique");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                await usersService.CreateUser(model);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {

            await authService.Logout(HttpContext);
            return RedirectToAction("Index", "Home");
        }
    }
}
