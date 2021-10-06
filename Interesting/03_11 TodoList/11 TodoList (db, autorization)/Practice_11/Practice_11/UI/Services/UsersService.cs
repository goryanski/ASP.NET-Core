using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_11.Business.DTO;
using Practice_11.Business.Services.Interfaces;
using Practice_11.Models.Auth;
using Practice_11.Models.Users.Auth;
using Practice_11.UI.Services.Interfaces;

namespace Practice_11.UI.Services
{
    public class UsersService : IUsersService
    {
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;
        IUsersDbService usersDbService;

        public UsersService(IUsersDbService usersDbService)
        {
            this.usersDbService = usersDbService;
        }
        public async Task CreateUser(RegisterViewModel model)
        {
            await usersDbService.CreateUser(model.Email, model.Password);
        }

        public async Task<int> GetUserIdByEmail(string email)
        {
            return await usersDbService.GetUserIdByEmail(email);
        }
    }
}
