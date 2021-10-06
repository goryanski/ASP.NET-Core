using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice.App.Models;

namespace Practice.App
{
    public class UsersStorage
    {
        private static UsersStorage instance;
        public static UsersStorage Instance { get => instance ?? (instance = new UsersStorage()); }

        public List<User> Users { get; set; }
        const int USERS_COUNT = 10;
        private UsersStorage()
        {
            Users = new List<User>();
            InitUsers();
        }

        private void InitUsers()
        {
            Random rnd = new Random();
            for (int i = 0; i < USERS_COUNT; i++)
            {
                Users.Add(new User
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = $"FirstName {i + 1}",
                    LastName = $"LastName {i + 1}",
                    Age = i + rnd.Next(10, 15)
                });
            }
        }
    }
}