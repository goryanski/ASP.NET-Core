using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions.Dto.Book;
using Services.Abstractions.Service;

namespace Presentation.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        readonly IServiceManager serviceManager;
        public BooksController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IEnumerable<BookDto>> GetAll()
        {
            return await serviceManager.BooksService.GetAllAsync();
        }

        [HttpPost]
        public async Task<BookDto> Create(CreateBookDto model)
        {
            return await serviceManager.BooksService.CreateAsync(model);
        }
    }
}
