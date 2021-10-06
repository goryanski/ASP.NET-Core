using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Practice_04.Models.Products;
using Practice_04.Services.Interfaces;
using Practice_04.Storage.Models;

namespace Practice_04.Services.Implementation
{
    public class SessionService : ISessionService
    {
        public void AddProductToBasket(HttpContext context, string key, ProductBasketViewModel value)
        {
            if (context.Session.GetString(key) is null)
            {
                context.Session.SetString(key, JsonConvert.SerializeObject(new List<ProductBasketViewModel>()));
            }

            var cartProducts = JsonConvert.DeserializeObject<List<ProductBasketViewModel>>(context.Session.GetString(key));
            cartProducts.Add(value);
            context.Session.SetString(key, JsonConvert.SerializeObject(cartProducts));
        }

        public List<ProductBasketViewModel> GetBasketProducts(HttpContext context, string key)
        {
            string json = context.Session.GetString(key);
            return json is null ? new List<ProductBasketViewModel>() : JsonConvert.DeserializeObject<List<ProductBasketViewModel>>(json);
        }
    }
}
