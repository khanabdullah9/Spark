using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Spark.Data;
using Spark.ViewModels;
using System.Security.Claims;

namespace Spark.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public AccountController(AppDbContext context,UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register() 
        {
            var model = new UserRegisterViewModel();
            var all_states = from s in IndianStates.States() select s;
            foreach (var a in all_states)
            {
                model.State.Add(new SelectListItem { Text=a,Value=a});
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegisterViewModel model) 
        {
            if (ModelState.IsValid) 
            {
                var user = new ApplicationUser()
                {
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    City = model.City,
                    State = model.state,
                    Address = model.Address,
                    PinCode = model.PinCode,
                };
                var allclaims = from c in ClaimsStore.Claim() select c;
                Claim claim = null;
                foreach (var cl in allclaims) 
                {
                    if (cl.Value.Equals("User")) 
                    {
                        claim = cl;
                    }
                }
                var result = await userManager.CreateAsync(user,model.Password);
                if (result.Succeeded)
                {
                    await userManager.AddClaimAsync(user,claim);
                    return RedirectToAction("index", "home");
                }
                else 
                {
                    foreach (var error in result.Errors) 
                    {
                        ModelState.AddModelError(String.Empty,error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login() 
        {
            var model = new UserLoginViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginViewModel model) 
        {
            if (ModelState.IsValid) 
            {
                var result = await signInManager.PasswordSignInAsync(model.Email,model.Password,false,false);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }
                else 
                {
                    ModelState.AddModelError(String.Empty,"Invalid login credential");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout() 
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index","home");
        }
        
        [HttpGet]
        public IActionResult AccessDenied() 
        {
            return View();
        }
    }
}
