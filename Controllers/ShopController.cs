using Microsoft.AspNetCore.Mvc;
using Spark.Data;
using Spark.Models;
using Spark.ViewModels;

namespace Spark.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext context;
        public ShopController(AppDbContext context) 
        {
            this.context = context;
        }
        public IActionResult Shop()
        {
            var allproducts = context.products;
            return View(allproducts);
        }

        [HttpGet]
        public IActionResult ViewProduct(string id) 
        {
            var allproducts = context.products;
            Product product = null;
            foreach (var prod in allproducts) 
            {
                if (prod.ProductId.Equals(id)) 
                {
                    product = prod;
                }
            }
            if (product==null) 
            {
                return View("NotFound");
            }
            var model = new ViewProductViewModel() 
            {
                ProductName = product.ProductName,
                ProductDescription = product.Description,
                Price = product.Price,
                Developer = product.Developer,
                Publisher = product.Publisher,
                Genre = product.Genre,
                product_image_path = product.product_image_path,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ViewProduct(ViewProductViewModel model) 
        {
            if (ModelState.IsValid) { }
            return View(model);
        }
    }
}
