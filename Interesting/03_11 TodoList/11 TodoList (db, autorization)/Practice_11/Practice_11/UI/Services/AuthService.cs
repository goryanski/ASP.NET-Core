using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Practice_11.Business.DTO;
using Practice_11.Business.Services.Interfaces;
using Practice_11.Models.Auth;
using Practice_11.Models.Users.Auth;
using Practice_11.UI.Services.Interfaces;

namespace Practice_11.UI.Services
{
    public class AuthService : IAuthService
    {
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;
        IAuthDbService authDbService;

        public AuthService(IAuthDbService authDbService)
        {
            this.authDbService = authDbService;
        }

        public async Task<bool> UserNotFound(string email)
        {
            return await authDbService.UserNotFound(email);
        }
        public async Task<bool> IncorrectUserPassword(string email, string password)
        {
            return await authDbService.IncorrectUserPassword(email, password);
        }

        public async Task Authentication(string email, HttpContext context)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, email)
            };

            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task Logout(HttpContext context)
        {
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<bool> IsUserEmailUnique(string email)
        {
            return await authDbService.IsUserEmailUnique(email);
        }
    }
}
