using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Practice_17.Models;
using Services.Abstractions.Service;

namespace Practice_17.Controllers
{
    public class HomeController : Controller
    {
        // Dbinit - UnitOfWork
        private readonly ILogger<HomeController> _logger;
        readonly IServiceManager serviceManager;
        public HomeController(ILogger<HomeController> logger, IServiceManager serviceManager)
        {
            _logger = logger;
            this.serviceManager = serviceManager;
        }

        public async Task<IActionResult> Index()
        {
            var accounts = (await serviceManager.AccountsService.GetAllAsync()).ToList();
            var autors = (await serviceManager.AuthorsService.GetAllAsync()).ToList();
            var books = (await serviceManager.BooksService.GetAllAsync()).ToList();
            var useBooks = (await serviceManager.UseBooksService.GetAllAsync()).ToList();
            ;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
