using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions.Dto.Account;
using Services.Abstractions.Service;

namespace Presentation.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        readonly IServiceManager serviceManager;
        public AccountsController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IEnumerable<AccountDto>> GetAll()
        {
            return await serviceManager.AccountsService.GetAllAsync();
        }

        [HttpPost]
        public async Task<AccountDto> Create(CreateAccountDto model)
        {
            return await serviceManager.AccountsService.CreateAsync(model);
        }
    }
}
