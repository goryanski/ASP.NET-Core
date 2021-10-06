using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Practice2.Models;

namespace Practice2.Business.Interfaces
{
    public interface IUsersService
    {
        string GetMainPage(HttpRequest request);
        string GetuserAccountPage(User user);
        string GetAuthorizationPage();
        string TryAddUser(string userLogin, string userPassword);
        string CheckUserData(string userLogin, string userPassword);
        User GetUserByLogin(string userLogin);
    }
}
