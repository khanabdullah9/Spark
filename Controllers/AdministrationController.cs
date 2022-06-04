using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spark.Data;
using Spark.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Spark.Models;

namespace Spark.Controllers
{
    [Authorize(Policy="Admin")]
    public class AdministrationController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;
        private readonly AppDbContext context;
        public AdministrationController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,AppDbContext context) 
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.hostingEnvironment = hostingEnvironment;
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ListUsers() 
        {
            var all_users = userManager.Users;
            return View(all_users);
        }

        [HttpGet]
        public IActionResult AssignClaim() 
        { 
            var model = new AssignClaimViewModel();
            var all_claims = from c in ClaimsStore.Claim() select c;
            foreach (var ac in all_claims) 
            {
                model.Claim.Add(new SelectListItem { Text = ac.Value, Value = ac.Value });
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignClaim(AssignClaimViewModel model) 
        {
            if (ModelState.IsValid) 
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                var allclaims = from c in ClaimsStore.Claim() select c;
                Claim claim = null;
                foreach (var ac in allclaims) 
                {
                    if (ac.Value.Equals(model.claim)) 
                    {
                        claim = ac;
                    }
                }
                if (user == null || claim == null) 
                {
                    return View("Error");
                }
                var result = await userManager.AddClaimAsync(user,claim);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "administration");
                }
                else if(!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(String.Empty, error.Description);
                    }
                }    
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult AddProduct() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(AddProductViewModel model) 
        {
            if (ModelState.IsValid) 
            {
                string uniqueFileName = null;
                if (model.ProductImage!=null) 
                {
                    string uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProductImage.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    model.ProductImage.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                string custom_id = Guid.NewGuid().ToString();
                Product product = new Product() 
                {
                    ProductId = custom_id,
                    ProductName = model.ProductName,
                    Genre = model.Genre,
                    Description = model.ProductDescription,
                    Publisher = model.Publisher,
                    Developer = model.Developer,
                    Quantity = model.Quantity,
                    Price = model.Price,
                    product_image_path = uniqueFileName,
                };
                context.products.Add(product);
                context.SaveChanges();
                return RedirectToAction("index","administration");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ListProduct() 
        {
            var allproducts = context.products;
            return View(allproducts);
        }
    }
}
