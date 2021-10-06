using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Services.Abstractions.Dto.Account;

namespace Services.Abstractions.Service
{
    public interface IAccountsService
    {
        Task<IEnumerable<AccountDto>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
