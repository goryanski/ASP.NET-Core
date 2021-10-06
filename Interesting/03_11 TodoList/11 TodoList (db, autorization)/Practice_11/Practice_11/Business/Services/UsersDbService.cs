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
    public class UsersDbService : IUsersDbService
    {
        IUnitOfWork uow;
        public UsersDbService(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public async Task CreateUser(string email, string password)
        {
            User user = new User
            {
                Email = email,
                Password = password
            };
            await uow.UsersRepository.CreateAsync(user);
        }

        public async Task<int> GetUserIdByEmail(string email)
        {
            User user = await uow.UsersRepository.GetUserByEmail(email);
            return user.Id;
        }
    }
}
