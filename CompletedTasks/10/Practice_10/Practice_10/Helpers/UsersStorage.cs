using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_10.Helpers.Models;

namespace Practice_10.Helpers
{
    public class UsersStorage
    {
        private static UsersStorage instance;
        public static UsersStorage Instance { get => instance ?? (instance = new UsersStorage()); }

        public List<User> Users { get; set; }
        private UsersStorage()
        {
            InitUsers();
        }

        private void InitUsers()
        {
            Users = new List<User>
            {
                new User
                {
                    Login = "igor",
                    Password = "1111",
                    Age = 22,
                    SecretQuestion = "Secret Question 1",
                    SecretQuestionAnswer = "answer 1"
                },
                new User
                {
                    Login = "vasya",
                    Password = "2222",
                    Age = 19,
                    SecretQuestion = "Secret Question 2",
                    SecretQuestionAnswer = "answer 2"
                },
                new User
                {
                    Login = "dimon_12",
                    Password = "3333",
                    Age = 21,
                    SecretQuestion = "Secret Question 3",
                    SecretQuestionAnswer = "answer 3"
                }
            };
        }

        internal void AddNewUser(User user)
        {
            Users.Add(user);
        }

        internal User GetUserByLogin(string userLogin)
        {
            return Users.FirstOrDefault(u => u.Login == userLogin);
        }
    }
}
