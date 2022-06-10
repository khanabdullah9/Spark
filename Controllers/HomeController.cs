using Microsoft.AspNetCore.Mvc;
using Spark.Data;
using Spark.Models;
using System.Diagnostics;
using System.Web;

namespace Spark.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext context;

        public HomeController(ILogger<HomeController> logger,AppDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            HttpContext.Session.SetString("init", "INIT");
            ViewData["sessionId"] = HttpContext.Session.Id;
            var allproducts = context.products;
            var filteredproducts = allproducts.Where(p => p.isCartItem.Equals(false)).Select(p=>p);
            return View(filteredproducts);
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
        [HttpGet]
        public IActionResult NotFound() 
        {
            ViewData["sessionId"] = HttpContext.Session.Id;
            return View();
        }
    }
}