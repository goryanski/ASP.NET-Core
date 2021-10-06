using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Practice_11.Models.Auth;
using Practice_11.Models.Users.Auth;

namespace Practice_11.UI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> UserNotFound(string email);
        Task<bool> IncorrectUserPassword(string email, string password);
        Task Authentication(string email, HttpContext context);
        Task Logout(HttpContext context);
        Task<bool> IsUserEmailUnique(string email);

    }
}
