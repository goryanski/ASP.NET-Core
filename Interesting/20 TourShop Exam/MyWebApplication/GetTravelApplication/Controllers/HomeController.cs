using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GetTravelApplication.Models;
using GetTravelApplication.Models.Tour;
using GetTravelApplication.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GetTravelApplication.Controllers
{
    public class HomeController : Controller
    {
        readonly IServiceViewManager serviceManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IServiceViewManager serviceManager, ILogger<HomeController> logger)
        {
            this.serviceManager = serviceManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int stars, int page = 1)//(int page = 1, int stars = 5)
        {
            // only first time if there are no cookie or when you click on bootstrap pagination element, otherwise - we will take the cookie on view page
            if (stars == 0)
            {
                var cookie = HttpContext.Request.Cookies["currentTourStarsCount"];
                if (cookie is null)
                {
                    stars = 5;
                }
                else
                {
                    stars = int.Parse(cookie);
                }
            }
            // tours count on page
            int pageSize = 2;  

            var tours = await serviceManager.ToursService.GetToursByStarsCount(stars);

            // skip elements until we find what we need and take as much as we want to display on page (= pageSize)
            var selectedTours = tours.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(tours.Count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Tours = selectedTours
            };
            
            // send object IndexViewModel (that holds all info about pagination) to the view
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
