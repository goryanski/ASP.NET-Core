using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice2.Models;

namespace Practice2.Helpers
{
    public class UsersStorage
    {
        private static UsersStorage instance;
        public static UsersStorage Instance { get => instance ?? (instance = new UsersStorage()); }

        public List<User> Users { get; set; }
        private User currentUser = null;
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
                    Id = Guid.NewGuid().ToString(),
                    Login = "igor",
                    Password = "1111",
                    Label = string.Empty
                },
                new User 
                { 
                    Id = Guid.NewGuid().ToString(), 
                    Login = "vasya", 
                    Password = "2222",
                    Label = string.Empty
                },
                new User 
                { 
                    Id = Guid.NewGuid().ToString(), 
                    Login = "dimon_12", 
                    Password = "3333",
                    Label = string.Empty
                }
            };  
        }
         
        internal void SetLabelsForUsers(List<string> authorizationUsersLogins)
        {
            foreach (var login in authorizationUsersLogins)
            {
                User user = GetUserByLogin(login);
                if (user != null)
                {
                    user.Label = "Is logged in";
                }
            }
        }

        internal string TryAddUser(string userLogin, string userPassword)
        {
            if(!UserExists(userLogin))
            {
                Users.Add(new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Login = userLogin,
                    Password = userPassword
                });
                return "success";
            }
            else
            {
                return "User already exists";
            }
        }

        internal User GetUserByLogin(string userLogin)
        {
            return Users.FirstOrDefault(u => u.Login == userLogin);
        }

        public bool IsPasswordCorrect(string userLogin, string userPassword)
        {
            return currentUser.Password == userPassword;
        }

        public bool UserExists(string userLogin)
        {
            currentUser = Users.FirstOrDefault(u => u.Login == userLogin);
            return !(currentUser is null);
        }
    }
}
