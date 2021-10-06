using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entity;
using Domain.Repository;
using Services.Abstractions.Dto.Account;
using Services.Abstractions.Service;

namespace Services
{
    internal sealed class AccountsService : IAccountsService
    {
        private readonly IUnitOfWork unitOfWork;
        private Automapper.ObjectMapper objectMapper = Automapper.ObjectMapper.Instance;

        public AccountsService(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }

        public async Task<AccountDto> CreateAsync(CreateAccountDto model, CancellationToken cancellationToken = default)
        {
            var entity = objectMapper.Mapper.Map<Account>(model);
            var result = await unitOfWork.AccountsRepository.Create(entity);
            return objectMapper.Mapper.Map<AccountDto>(result);
        }

        public async Task<IEnumerable<AccountDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var result = await unitOfWork.AccountsRepository.GetAllAsync();
            return objectMapper.Mapper.Map<IEnumerable<AccountDto>>(result);
        }
    }
}
