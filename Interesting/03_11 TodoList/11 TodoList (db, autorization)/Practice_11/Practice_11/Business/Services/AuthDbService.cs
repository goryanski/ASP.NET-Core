using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_11.Business.DTO;
using Practice_11.Business.Services.Interfaces;
using Practice_11.DB.Entities;
using Practice_11.DB.Repositories;

namespace Practice_11.Business.Services
{
    public class AuthDbService : IAuthDbService
    {
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;
        IUnitOfWork uow;
        public AuthDbService(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public Task<User> GetUserByEmail(string email)
           => uow.UsersRepository.GetUserByEmail(email);

        public async Task<bool> UserNotFound(string email)
        {
            return await GetUserByEmail(email) == null;
        }

        public async Task<bool> IncorrectUserPassword(string email, string password)
        {
            var user = await GetUserByEmail(email);
            return user.Password != password;
        }

        public async Task<bool> IsUserEmailUnique(string email)
        {
            return await UserNotFound(email);
        }
    }
}
