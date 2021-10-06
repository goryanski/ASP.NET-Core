using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions.Dto.Author;
using Services.Abstractions.Service;

namespace Presentation.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        readonly IServiceManager serviceManager;
        public AuthorsController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IEnumerable<AuthorDto>> GetAll()
        {
            return await serviceManager.AuthorsService.GetAllAsync();
        }

        [HttpPost]
        public async Task<AuthorDto> Create(CreateAuthorDto model)
        {
            return await serviceManager.AuthorsService.CreateAsync(model);
        }
    }
}
