using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Practice2.Helpers;
using Practice2.Models;

namespace Practice2.Middleware
{
    public class AuthMiddleware
    {
        UsersStorage usersStorage = UsersStorage.Instance;
        private readonly RequestDelegate _next;
        
        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // 1. Check if user exists in UsersStorage
            string userLogin = context.Request.Query["login"].ToString();
            User user = usersStorage.Users.FirstOrDefault(u => u.Login == userLogin);
            if (user is null)
            {
                // bad request => user is not logged in => go to registration
                context.Response.Redirect("/registration");
            }
            else
            {
                // 2. Check if user is authorized (there're user's cookie)
                var cookieLogin = context.Request.Cookies[userLogin];
                if (cookieLogin is null)
                {
                    // go to login form
                    context.Response.Redirect("/login");
                }
                else
                {
                    // go to user account
                    context.Items.Add("selectedUser", user);
                    await _next.Invoke(context);
                }
            }
        }
    }
}
