using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Practice2.Business.Interfaces;
using Practice2.Helpers;
using Practice2.Models;

namespace Practice2.Business.Implementation
{
    public class UsersService: IUsersService
    {
        private UsersStorage usersStorage = UsersStorage.Instance;
        private UIHelper uiHelper = new UIHelper();

        public string GetMainPage(HttpRequest request)
        {
            var cookies = request.Cookies;
            List<string> authorizationUsersLogins = new List<string>();
            foreach (var item in cookies)
            {
                authorizationUsersLogins.Add(item.Key);
            }
            usersStorage.SetLabelsForUsers(authorizationUsersLogins);

            return uiHelper.GetUsersListHtml(usersStorage.Users, request.Host.Value)
            + uiHelper.GetUserRegistrationButton();
        }

        public string CheckUserData(string userLogin, string userPassword)
        {
            if (usersStorage.UserExists(userLogin))
            {
                if (usersStorage.IsPasswordCorrect(userLogin, userPassword))
                {
                    return "success";
                }
                else
                {
                    return "Wrong password";
                }
            }
            else
            {
                return "User not found";
            }
        }
        public string GetuserAccountPage(User user)
            => uiHelper.GetuserAccountPage(user);

        public string GetAuthorizationPage()
            => uiHelper.GetRegistrationPage();

        public string TryAddUser(string userLogin, string userPassword)
            => usersStorage.TryAddUser(userLogin, userPassword);

        public User GetUserByLogin(string userLogin)
            => usersStorage.GetUserByLogin(userLogin);
    }
}
