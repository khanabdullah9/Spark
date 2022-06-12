using Microsoft.AspNetCore.Mvc.Rendering;
using Spark.Models;

namespace Spark.ViewModels
{
    public class CartProductViewModel
    {
        public CartProductViewModel()
        {
            Quantity = new List<SelectListItem>();
        }
        public List<CartItem> cartItems { get; set; }
        public List<Product> products { get; set; }
        public int quantity { get; set; }//To send product quantity from the view to the controller ONLY
        public List<SelectListItem> Quantity { get; set; }
    }
}
