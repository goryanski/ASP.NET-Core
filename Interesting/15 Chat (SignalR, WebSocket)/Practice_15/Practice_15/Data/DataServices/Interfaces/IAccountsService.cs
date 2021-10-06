using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_15.Data.Entity;
using Practice_15.Models.Account;

namespace Practice_15.Data.DataServices.Interfaces
{
    public interface IAccountsService
    {
        User FindUser(LoginViewModel model);
        User FindUserByName(string authUsername);
        User GetUserById(int id);
    }
}
