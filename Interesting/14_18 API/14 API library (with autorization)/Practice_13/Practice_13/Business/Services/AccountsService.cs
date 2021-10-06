using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_13.Business.DTO;
using Practice_13.Business.Services.Interfaces;
using Practice_13.DB.Repositories.Interfaces;

namespace Practice_13.Business.Services
{
    public class AccountsService: IAccountsService
    {
        private IUnitOfWork uow;
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;

        public AccountsService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<AccountDTO> GetAccount(string username)
        {
            var result = await uow.AccountsRepository.GetAllAsync(u => u.Username.Equals(username));
            if(result.Count == 0)
            {
                return null;
            }
            return objectMapper.Mapper.Map<AccountDTO>(result.First());
        }

        public async Task<List<AccountDTO>> GetAllAccounts()
        {
            var result = await uow.AccountsRepository.GetAllAsync();
            return objectMapper.Mapper.Map<List<AccountDTO>>(result);
        }
    }
}
