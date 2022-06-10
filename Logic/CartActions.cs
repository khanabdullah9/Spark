using Spark.Data;
using Spark.Models;
using System.Diagnostics;

namespace Spark.Logic
{
    public class CartActions : IDisposable
    {
        private readonly AppDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        public CartActions(AppDbContext context , IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        /*
         Params: ID -> ID of the cart (cookie session ID)
        Output: Cart -> return the Cart object
        Create a new Cart object if the cart does not exist yet.
         */
        public Cart GetCart(string ID) 
        {
            Cart cart = context.carts.Where(c => c.ID == ID).Select(c => c).FirstOrDefault();
            Cart result = null;
            if (cart is null)
            {
                Cart new_cart = new Cart()
                {
                    ID = ID,
                };
                context.carts.Add(new_cart);
                context.SaveChanges();
                result = new_cart;
                Debug.WriteLine("[DEBUG{CartActions:35}]cart is null");
            }
            else if (cart is not null)
            {
                result = cart;
            }
            return result;
        }

        /*
         Params: ID -> ID of the cart (cookie session ID)
        prodID -> ID of the product to be added
        cart -> Cart to add product to
        Output: bool -> whether the product has been added or not
        Create a new Cart object if the cart does not exist yet.
         */
        public void AddToCart(string ID,string prodID,Cart cart) 
        {
            var product_query = context.products.Where(p => p.ProductId == prodID).Select(p=>p);
            Product product = null;
            foreach (var prod in product_query) 
            {
                product = prod;
            }
            if (product is null) { Debug.WriteLine("[DEBUG{CartActions:39}]product is null"); }
            CartItem cartItem = new CartItem()
            {
                ID = Guid.NewGuid().ToString(),
                Product = product,
                productID = prodID,
                Quantity = 1,
                CartId = ID,//foreign key property
                Cart = cart,
            };           
            //items.Add(cartItem);
            context.cartItems.Add(cartItem);
            context.SaveChanges();
        }

        /*
         Params: ID -> ID of the cart
         */
        public List<CartItem> GetCartItems(string ID) 
        {
            List<CartItem> cartItems = new List<CartItem>();
            var cartItemS = context.cartItems.Where(c => c.CartId == ID).Select(c => c);
            foreach (var item in cartItemS) 
            {
                cartItems.Add(item);
            }
            foreach (var item in cartItems) 
            {
                Debug.WriteLine("[PRODUCT{CartAction:82}] ID:"+item.ID);
                Debug.WriteLine("[PRODUCT{CartAction:83}] CartId:" + item.CartId);
                Debug.WriteLine("[PRODUCT{CartAction:84}] Product ID:" + item.productID);
            }
            return cartItems;
        }

        /*
         Params: ID
        return the total price of the cart
         */
        public double TotalPrice(string ID) 
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
