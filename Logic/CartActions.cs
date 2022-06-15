using Spark.Data;
using Spark.Models;
using System.Diagnostics;

namespace Spark.Logic
{
    public class CartActions : IDisposable
    {
        private readonly AppDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private int _cartItemQuantity;
        public CartActions(AppDbContext context , IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        /*
         * Params: ID -> ID of the cart (cookie session ID)
         * Output: Cart -> return the Cart object
         * Create a new Cart object if the cart does not exist yet.
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
         * Params: ID -> ID of the cart (cookie session ID)
         * prodID -> ID of the product to be added
         * cart -> Cart to add product to
         * Output: bool -> whether the product has been added or not
         * Create a new Cart object if the cart does not exist yet.
         */
        public void AddToCart(string ID,string prodID,Cart cart) 
        {
            Debug.WriteLine("[INVOKE{CartAction:54}]AddToCart invoked.....");
            var product_query = context.products.Where(p => p.ProductId == prodID).Select(p => p);
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
            var oldCartItem = context.cartItems.Where(c => c.CartId == ID && c.productID == prodID).Select(c => c).FirstOrDefault();
            if (oldCartItem is null) //If this is a fresh CartItem
            {
                context.cartItems.Add(cartItem);
                context.SaveChanges();
            }
        }

        /*
         * Params: ID -> ID of the cart
         */
        public List<CartItem> GetCartItems(string ID) 
        {
            List<CartItem> cartItems = new List<CartItem>();
            var cartItemS = context.cartItems.Where(c => c.CartId == ID).Select(c => c);
            foreach (var item in cartItemS) 
            {
                cartItems.Add(item);
            }
            foreach (var item in cartItems){Debug.WriteLine("[PRODUCT{CartAction:82}] ID:"+item.ID);Debug.WriteLine("[PRODUCT{CartAction:83}] CartId:" + item.CartId);Debug.WriteLine("[PRODUCT{CartAction:84}] Product ID:" + item.productID);}
            return cartItems;
        }

        /*
         * Params: ID -> ID the Cart
         * return the total price of the cart
         */
        public double GetTotal(string ID) 
        {
            double totalPrice = 0;
            var query = context.cartItems.Where(c => c.CartId == ID).Select(c=>c);
            foreach (var each in query) 
            {
                totalPrice += each.Product.Price * each.Quantity;
            }
            return totalPrice;
        }

        /*
         * Params: ID -> CartId of the CartItem the product belongs to a.k.a sessionID
         * return the quantity of the Product in the CartItems
         */
        public Dictionary<string, double> GetQuantity(string CartId)
        {
            var quantity_query = context.cartItems.Where(c => c.CartId == CartId).Select(c => c);
            Dictionary<string,double> productsAndQuantities = new Dictionary<string, double>();
            foreach (var item in quantity_query) 
            {
                productsAndQuantities.Add(item.Product.ProductName,item.Product.Price*item.Quantity);
            }
            if (quantity_query is null) { Debug.WriteLine("[DEBUG{CartAction:105}]quantity_query is null"); }
            return productsAndQuantities;
        }

        /*
         * Params: ID -> ID of the CartItem
         * Increase the quantity of the CartItem
         */
        public void IncrementQuantity(string ID) 
        {
            Debug.WriteLine("[INVOKE{CartAction:115}]IncrementQuantity invoked..... with CartItem ID = "+ID);
            var quantity_query = context.cartItems.Where(c => c.ID == ID).Select(c => c).FirstOrDefault();
            int quantity = quantity_query.Quantity;
            quantity = quantity + 1;
            Debug.WriteLine("[INVOKE{CartAction:119}]quantity updated to " + quantity);
            if (quantity_query is null) { Debug.WriteLine("[DEBUG{CartAction:118}]quantity_query is null"); }
            context.SaveChanges();
        }

        /*
         * Params: ID -> ID of the CartItem
         * Decrement the quantity of the CartItem
         */
        public void DecrementQuantity(string ID)
        {
            Debug.WriteLine("[INVOKE{CartAction:129}]DecrementQuantity invoked..... with CartItem ID = " + ID);
            var quantity_query = context.cartItems.Where(c => c.ID == ID).Select(c => c).FirstOrDefault();
            int quantity = quantity_query.Quantity;
            quantity = quantity - 1;
            Debug.WriteLine("[INVOKE{CartAction:134}]quantity updated to " + quantity);
            if (quantity_query is null) { Debug.WriteLine("[DEBUG{CartAction:118}]quantity_query is null"); }
            context.SaveChanges();
        }


        /*
         * Params: ID -> ID of the Cart (session ID)
         * prodId -> ID of the product within the CartItem
         * return the CartItem ID
         */
        public string GetCartItemID(string ID,string prodID) 
        {
            var cartItem_query = context.cartItems.Where(c => c.CartId == ID && c.productID == prodID).Select(c=>c).FirstOrDefault();
            if (cartItem_query is null) { Debug.WriteLine("[DEBUG{CartAction:144}]cartItem_query is null"); }
            return cartItem_query.ID;
        }

        /*
         * params: ID -> ID of the cart
         * prodID -> ID of the product in the CartItem
         Remove a cart item from the cart
         */
        public void Remove(string ID,string prodID) 
        {
            var cartItem = context.cartItems.Where(c => c.CartId == ID && c.productID == prodID).Select(c=>c).FirstOrDefault();
            context.cartItems.Remove(cartItem);
            context.SaveChanges();
        }
        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
