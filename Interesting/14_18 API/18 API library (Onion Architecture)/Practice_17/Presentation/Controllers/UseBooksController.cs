using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions.Dto.UseBook;
using Services.Abstractions.Service;

namespace Presentation.Controllers
{
    [Route("api/useBooks")]
    [ApiController]
    public class UseBooksController : ControllerBase
    {
        readonly IServiceManager serviceManager;
        public UseBooksController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IEnumerable<UseBookDto>> GetAll()
        {
            return await serviceManager.UseBooksService.GetAllAsync();
        }

        [HttpPost]
        public async Task<UseBookDto> Create(CreateUseBookDto model)
        {
            return await serviceManager.UseBooksService.CreateAsync(model);
        }
    }
}
