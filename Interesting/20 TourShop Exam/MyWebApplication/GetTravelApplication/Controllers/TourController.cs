using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetTravelApplication.Models;
using GetTravelApplication.Models.Search;
using GetTravelApplication.Models.Tour;
using GetTravelApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace GetTravelApplication.Controllers
{
    public class TourController : Controller
    {
        readonly IServiceViewManager serviceManager;

        static List<TourShortInfoViewModel> tours = null;
        static TourSrchParamsModel filter = null;

        public TourController(IServiceViewManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        public async Task<IActionResult> ShowTour(string tourId)
        {
            var tour = await serviceManager.ToursService.GetTour(Guid.Parse(tourId));
            return View(tour);
        }

        public async Task<IActionResult> ProSearch(int page = 1)
        {
            var countries = (await serviceManager.CountriesViewService.GelAll())
                .OrderBy(c => c.Name)
                .ToList();


            ProSearchViewModel pageViewModel = new ProSearchViewModel
            {
                Countries = countries
            };

            if (tours != null)
            {
                int pageSize = 3;

                var selectedTours = tours.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                PageViewModel pageView = new PageViewModel(tours.Count, page, pageSize);

                pageViewModel.Tours = selectedTours;
                pageViewModel.PageViewModel = pageView;
                pageViewModel.Filter = filter;
            }
            return View(pageViewModel);
        }


        public async Task<IActionResult> StartSrch(TourSrchParamsModel srchParams)
        {
            filter = srchParams;
            tours = await serviceManager.ToursService.GetSrchTours(srchParams);
            return RedirectToAction("ProSearch");
        }
    }


}
