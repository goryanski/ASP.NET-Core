using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Practice_04.Models;
using Practice_04.Models.Products;
using Practice_04.Services.Interfaces;
using Practice_04.Storage;
using Practice_04.Storage.Models;

namespace Practice_04.Controllers
{
    public class BasketController : Controller
    {
        ISessionService _sessionService;
        ProductsStorage _productsStorage;
        public BasketController(ISessionService sessionService, ProductsStorage productsStorage)
        {
            _sessionService = sessionService;
            _productsStorage = productsStorage;
        }

        [HttpPost]
        public IActionResult AddToBasket(string id)
        {
            if (id is null) return BadRequest();
            Product product = _productsStorage.GetProductById(id);

            if (product is null)
            {
                return NotFound($"Product not found");
            }
            _sessionService.AddProductToBasket(HttpContext, "cart", new ProductBasketViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Rating = product.Rating,
                Photo = product.Photo
            });

            StringValues referer;
            HttpContext.Request.Headers.TryGetValue("referer", out referer);
            return Redirect(referer.First());  
        }
    }
}
