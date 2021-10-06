using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Practice_04.Models;
using Practice_04.Storage;
using Practice_04.Storage.Models;

namespace Practice_04.Controllers
{
    public class HomeController : Controller
    {
        private static string productNameToSearch = null;
        ProductsStorage _productsStorage;
        public HomeController(ProductsStorage productsStorage)
        {
            _productsStorage = productsStorage;
        }

        public IActionResult Index()
        {
            HomeIndexViewModel viewModel = new HomeIndexViewModel
            {
                Products = productNameToSearch is null ?
                    _productsStorage.Products :
                    _productsStorage.GetFoundProducts(productNameToSearch)
            };
            productNameToSearch = null;

            if (viewModel.Products.Count == 0)
            {
                return RedirectToAction("ProductsNotFound");
            }

            return View(viewModel);
        }

        public IActionResult SearchProduct(string stchText) 
        {
            ;
            if (stchText is null) return BadRequest();

            productNameToSearch = stchText;
            return RedirectToAction("Index");
         }

        public IActionResult ProductsNotFound()
        {
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
