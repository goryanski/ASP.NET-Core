using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_11.Models.Auth;
using Practice_11.Models.Users.Auth;

namespace Practice_11.UI.Services.Interfaces
{
    public interface IUsersService
    {
        Task CreateUser(RegisterViewModel model);
        Task<int> GetUserIdByEmail(string name);
    }
}
