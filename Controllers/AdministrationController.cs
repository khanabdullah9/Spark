using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spark.Data;
using Spark.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Spark.Controllers
{
    [Authorize(Policy="Admin")]
    public class AdministrationController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public AdministrationController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager) 
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
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
    }
}
