using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Practice_04.Services;
using Practice_04.Storage.Models;

namespace Practice_04.Storage
{
    public class ProductsStorage
    {
        public List<Product> Products { get; } = new List<Product>();

        public ProductsStorage()
        {
            InitProducts();
        }

        private void InitProducts()
        {
            Products.Add(new Product 
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Lorem",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                Price = 125.5m,
                Photo = "https://pngimg.com/uploads/ak47/ak47_PNG15449.png",
                Rating = 4
            });
            Products.Add(new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Ipsum",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                Price = 345.2m,
                Photo = "https://pngimg.com/uploads/ak47/ak47_PNG15449.png",
                Rating = 5
            });
            Products.Add(new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Dolor",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                Price = 200,
                Photo = "https://pngimg.com/uploads/ak47/ak47_PNG15449.png",
                Rating = 3
            });
        }

        internal void RemoveProduct(Product product) => Products.Remove(product);

        public List<Product> GetFoundProducts(string srchText)
        {
            return Products.Where(p => 
                p.Name.ToLower().Contains(srchText.ToLower()))
                .ToList();
        }

        internal Product GetProductById(string id) => Products.FirstOrDefault(p => p.Id == id);

        internal void EditProduct(Product product)
        {
            Product srchProduct = GetProductById(product.Id);
            Products[Products.IndexOf(srchProduct)] = product;
        }

        //public string GroupProducts() 
    }
}
