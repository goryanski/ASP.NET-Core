using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_10.Business.Interfaces;
using Practice_10.Helpers;
using Practice_10.Helpers.Models;
using Practice_10.Models.Users;

namespace Practice_10.Business
{
    public class UsersService : IUsersService
    {
        private UsersStorage usersStorage = UsersStorage.Instance;


        public User GetUserByLogin(string userLogin)
            => usersStorage.GetUserByLogin(userLogin);

        public bool IncorrectUserPassword(string login, string password)
        {
            return GetUserByLogin(login).Password != password;
        }

        public bool UserNotFound(string login)
        {
            return GetUserByLogin(login) == null;
        }

        public string GetSecretQuestion(string userLogin)
        {
            // such userLogin exists for sure
            return GetUserByLogin(userLogin).SecretQuestion;
        }

        public bool IsUserAnswerRight(string login, string secretQuestionAnswer)
        {
            return GetUserByLogin(login).SecretQuestionAnswer == secretQuestionAnswer;
        }

        public void AddNewUser(NewUserViewModel user)
        {
            usersStorage.AddNewUser(new User
            {
                Login = user.Login,
                Password = user.Password,
                Age = user.Age,
                SecretQuestion = user.SecretQuestion,
                SecretQuestionAnswer = user.SecretQuestionAnswer
            });
        }

        public bool IsUserLoginUnique(string login)
        {
            return UserNotFound(login);
        }
    }
}
