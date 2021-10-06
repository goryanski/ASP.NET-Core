using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Practice_11.Models;
using Practice_11.Models.Tasks;
using Practice_11.UI.Services.Interfaces;

namespace Practice_11.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ITasksService tasksService;
        IUsersService usersService;

        public HomeController(ILogger<HomeController> logger, 
            ITasksService tasksService, 
            IUsersService usersService)
        {
            _logger = logger;
            this.tasksService = tasksService;
            this.usersService = usersService;
        }

        public async Task<IActionResult> Index()
        {
            int authUserId = User.Identity.IsAuthenticated ? await usersService.GetUserIdByEmail(User.Identity.Name) : 0;
            var viewModel = new TasksIndexViewModel
            {
                Tasks = await tasksService.GetAllTasks(),
                AuthUserId = authUserId
            };
            return View(viewModel);
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
