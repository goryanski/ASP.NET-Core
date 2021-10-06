using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Practice_04.Models;
using Practice_04.Models.Products;
using Practice_04.Services;
using Practice_04.Services.Interfaces;
using Practice_04.Storage;
using Practice_04.Storage.Models;

namespace Practice_04.Controllers
{
    public class ProductController : Controller
    {
        ProductsStorage _productsStorage;
        IFileUpload _fileUpload;
        IFileHelper _fileHelper;
        public ProductController(ProductsStorage productsStorage, IFileUpload fileUpload, IFileHelper fileHelper)
        {
            _productsStorage = productsStorage;
            _fileUpload = fileUpload;
            _fileHelper = fileHelper;
        }

        public IActionResult Index(string id)
        {
            Product product = null;
            if (id != null)
            {
                product = _productsStorage.GetProductById(id);

                if (product is null)
                {
                    return NotFound($"Product not found");
                }
            }
            
            return View(new ProductIndexViewModel
            {
                Product = product
            });
        }
        public IActionResult DeleteProduct(string id)
        {
            if (id is null) return BadRequest();
            Product product = _productsStorage.GetProductById(id);

            if (product is null)
            {
                return NotFound($"Product not found");
            }

            _productsStorage.RemoveProduct(product);
            return Redirect("/Home/Index");
        }
       
        public IActionResult ViewProduct(string id)
        {
            if (id is null) return BadRequest();
            Product product = _productsStorage.GetProductById(id);

            if (product is null)
            {
                return NotFound($"Product not found");
            }

            return View(new HomeIndexViewModel
            {
                Product = product
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewProductViewModel product)
        {
            string photo = await _fileUpload.Upload(product.Photo);

            _productsStorage.Products.Add(new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = product.Name,
                Description = product.Description,
                Price = Convert.ToDecimal(product.Price),
                Rating = product.Rating,
                Photo = $"/{photo}"
            });
            return Redirect("/Home/Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NewProductViewModel product, string prevPhoto, string id)
        {
            string photo;
            if (product.Photo is null)
            {
                photo = prevPhoto;
            }
            else
            {
                _fileHelper.DeleteProductPrevPhoto(prevPhoto);
                photo = await _fileUpload.Upload(product.Photo);
            }

            _productsStorage.EditProduct(new Product
            {
                Id = id,
                Name = product.Name,
                Description = product.Description,
                Price = Convert.ToDecimal(product.Price),
                Rating = product.Rating,
                Photo = product.Photo is null ? photo : $"/{photo}"
            }); 
            return Redirect("/Home/Index");
        }
    }
}
