using Spark.Models;

namespace Spark.Logic
{
    public interface ICartActions
    {
        public Cart GetCart(string ID);
        public void AddToCart(string ID, string prodID, Cart cart);
        public List<CartItem> GetCartItems(string ID);
        public double TotalPrice(string ID);
        public int GetQuantity(string CartId);
        public void IncrementQuantity(string ID);
        public void DecrementQuantity(string ID);
        public string GetCartItemID(string ID, string prodID);
    }
}
