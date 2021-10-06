using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_15.Data.DataServices.Interfaces;
using Practice_15.Data.Entity;
using Practice_15.Models.Account;

namespace Practice_15.Data.DataServices
{
    public class AccountsService : IAccountsService
    {
        ApplicationDbContext db;
        public AccountsService(ApplicationDbContext context)
        {
            db = context;
        }
        
        public User FindUser(LoginViewModel model)
        {
            return db.Users.FirstOrDefault(u => u.Email.Equals(model.Email) && u.Password.Equals(model.Password));
        }

        public User FindUserByName(string authUsername)
        {
            // user exists for shure
            return db.Users.First(u => u.Email.Equals(authUsername));
        }

        public User GetUserById(int id)
        {
            return db.Users.First(u => u.Id == id);
        }
    }
}
