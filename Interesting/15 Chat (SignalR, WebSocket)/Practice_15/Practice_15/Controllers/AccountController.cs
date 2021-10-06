using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Practice_15.Data;
using Practice_15.Data.DataServices.Interfaces;
using Practice_15.Data.Entity;
using Practice_15.Models.Account;

namespace Practice_15.Controllers
{
    public class AccountController : Controller
    {
        IAccountsService accountsService;
        IChatService chatService;
        public AccountController(IAccountsService accountsService, IChatService chatService)
        {
            this.accountsService = accountsService;
            this.chatService = chatService;
        }

        public IActionResult Login()
        {
            //chatService.DbInit();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Password = "";
                return View(model);
            }

            var user = accountsService.FindUser(model);
            if (user is null)
            {
                return BadRequest();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, model.Email)
            };

            ClaimsIdentity id = new ClaimsIdentity(
                claims,
                "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
