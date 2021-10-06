using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_04.Models.Products;
using Practice_04.Storage.Models;

namespace Practice_04.Models
{
    public class BasketViewModel
    {
        public ProductBasketViewModel Product { get; set; }
        public List<ProductBasketViewModel> Products { get; set; }
    }
}
