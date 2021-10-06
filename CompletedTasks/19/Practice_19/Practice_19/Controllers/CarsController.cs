using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Practice_19.Models;
using Practice_19.Services;
using Practice_19.Utils;

namespace Practice_19.Controllers
{
    public class CarsController : Controller
    {
        readonly AppUtils utils;
        ICarsService carsService;
        public CarsController(AppUtils utils, ICarsService carsService)
        {
            this.utils = utils;
            this.carsService = carsService;
        }
        public IActionResult AddCar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCar(List<AddCarViewModel> carTranslateVersions)
        {
            if (carTranslateVersions.Count != 0)
            {
                await carsService.AddCar(carTranslateVersions);
            }
            return RedirectToAction("index", "home");
        }
    }
}
