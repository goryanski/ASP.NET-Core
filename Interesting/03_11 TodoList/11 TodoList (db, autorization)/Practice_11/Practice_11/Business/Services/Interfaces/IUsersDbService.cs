using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_11.Business.DTO;

namespace Practice_11.Business.Services.Interfaces
{
    public interface IUsersDbService
    {
        Task CreateUser(string email, string password);
        Task<int> GetUserIdByEmail(string email);
    }
}
