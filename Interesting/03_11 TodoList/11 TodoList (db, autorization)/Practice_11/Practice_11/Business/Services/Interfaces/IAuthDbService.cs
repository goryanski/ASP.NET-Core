using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_11.Business.DTO;

namespace Practice_11.Business.Services.Interfaces
{
    public interface IAuthDbService
    {
        Task<bool> UserNotFound(string email);
        Task<bool> IncorrectUserPassword(string email, string password);
        Task<bool> IsUserEmailUnique(string email);
    }
}
