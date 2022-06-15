using Spark.Models;

namespace Spark.Logic
{
    public interface ICartActions
    {
        public Cart GetCart(string ID);
        public void AddToCart(string ID, string prodID, Cart cart);
        public List<CartItem> GetCartItems(string ID);
        public double GetTotal(string ID);
        public Dictionary<string, double> GetQuantity(string CartId);
        public void IncrementQuantity(string ID);
        public void DecrementQuantity(string ID);
        public string GetCartItemID(string ID, string prodID);
        public void Remove(string ID, string prodID);
    }
}
