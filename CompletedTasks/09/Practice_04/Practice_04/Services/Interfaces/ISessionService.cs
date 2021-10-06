using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Practice_04.Models;
using Practice_04.Models.Products;
using Practice_04.Storage.Models;

namespace Practice_04.Services.Interfaces
{
    public interface ISessionService
    {
        void AddProductToBasket(HttpContext context, string key, ProductBasketViewModel value);
        List<ProductBasketViewModel> GetBasketProducts(HttpContext context, string key);
    }
}
