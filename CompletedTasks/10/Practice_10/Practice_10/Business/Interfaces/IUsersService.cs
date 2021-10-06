using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_10.Helpers.Models;
using Practice_10.Models.Users;

namespace Practice_10.Business.Interfaces
{
    public interface IUsersService
    {
        bool UserNotFound(string login);
        bool IncorrectUserPassword(string login, string password);
        User GetUserByLogin(string login);
        string GetSecretQuestion(string userLogin);
        bool IsUserAnswerRight(string login, string secretQuestionAnswer);
        void AddNewUser(NewUserViewModel user);
        bool IsUserLoginUnique(string login);
    }
}
