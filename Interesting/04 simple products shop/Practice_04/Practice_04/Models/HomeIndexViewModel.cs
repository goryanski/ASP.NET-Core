using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_04.Storage.Models;

namespace Practice_04.Models
{
    public class HomeIndexViewModel
    {
        public List<Product> Products { get; set; }
        public Product Product { get; set; }
    }
}
