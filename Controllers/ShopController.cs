using Microsoft.AspNetCore.Mvc;
using Spark.Data;
using Spark.Models;
using Spark.ViewModels;
using System.Diagnostics;
using Spark.Logic;

namespace Spark.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        public ShopController(AppDbContext context, IHttpContextAccessor httpContextAccessor) 
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Shop()
        {
            var allproducts = context.products;
            var filterproduct = allproducts.Where(p => p.isCartItem.Equals(false)).Select(p=>p);
            return View(filterproduct);
        }

        //Render the ViewProduct View
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
                return RedirectToAction("notfound","home");
            }
            var model = new ViewProductViewModel()
            {
                ID = product.ProductId,
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
        public IActionResult ViewProduct(ViewProductViewModel model) 
        {
            string ID = HttpContext.Session.Id;
            string prodID = model.ID;
            if (prodID is null) { Debug.WriteLine("[DEBUG{ShopController:63}]prodID is null"); }
            CartActions cartActions = new CartActions(this.context,this.httpContextAccessor);
            Cart cart = cartActions.GetCart(ID);
            cartActions.AddToCart(ID,prodID,cart);            
            return RedirectToAction(actionName: "cart", controllerName: "shop");
        }

        [HttpGet]
        public IActionResult Cart()
        {
            var model = new CartProductViewModel();
            string ID = HttpContext.Session.Id;
            ViewData["sessionID"] = ID;
            CartActions cartActions = new CartActions(this.context, this.httpContextAccessor);
            List<CartItem> cartItems = cartActions.GetCartItems(ID);
            List<Product> products = new List<Product>();
            ViewData["quantity"] = null;
            if (cartItems is null) { Debug.WriteLine("[DEBUG{ShopController:77}]cartItems is null"); }
            foreach (var item in cartItems) 
            {
                ViewData["quantity"] = item.Quantity;
                string productId = item.productID;
                var product_query = context.products.Where(p => p.ProductId == productId).Select(p=>p);
                Product product = null;
                foreach (var prod in product_query) 
                {
                    product = prod;
                    for (int num = 1; num <= prod.Quantity; num++)
                    {
                        model.Quantity.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Text = num.ToString(), Value = num.ToString() });
                    }
                }
                if (product is null) { Debug.WriteLine("[DEBUG{ShopController:81}]product is null"); }
                products.Add(product);
                
            }            
            model.products = products;
            return View(model);
        }

        /*
        params: prodID -> ID of the product in the CartItem
       quantity -> quantity of the product
        */
        [HttpPost]
        public IActionResult UpdateQuantity() 
        {
            Debug.WriteLine("[DEBUG{ShopController:155}] UpdateQuantity() invoked...");
            var sessionID = HttpContext.Session.Id;
            var cart = context.carts.Where(c => c.ID == sessionID).Select(c => c).FirstOrDefault();
            if (cart is null) { Debug.WriteLine("[DEBUG{ShopController:157}] Cart is null"); }
            var productID = HttpContext.Request.Form["ProductIn"].ToString();
            var cartItem = context.cartItems.Where(c => c.CartId == sessionID && c.productID == productID).Select(c => c).FirstOrDefault();
            if (cartItem is null) { Debug.WriteLine("[DEBUG{ShopController:157}] CartItem is null"); }
            var form_request = HttpContext.Request.Form["QuantityBtn"];
            Debug.WriteLine("[form_request{ShopController:162}] " + form_request+" for "+cartItem.ID);
            int quantity = int.Parse(form_request);
            cartItem.Quantity = quantity;
            context.SaveChanges();
            return RedirectToAction("cart", "shop");
        }

        [HttpPost]
        public IActionResult Remove() 
        {
            var ID = HttpContext.Request.Form["CartID"];
            var prodID = HttpContext.Request.Form["ProductID"];
            CartActions cartActions = new CartActions(this.context,this.httpContextAccessor);
            cartActions.Remove(ID,prodID);
            return RedirectToAction("cart","shop");
            Debug.WriteLine("[DEBUG{CartAction:134}]cartItem has been removed");
        }
        [HttpGet]
        public IActionResult Checkout() 
        {
            return View();
        }
    }
}
