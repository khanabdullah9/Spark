using System.ComponentModel.DataAnnotations;

namespace Spark.Models
{//Principal Entity
    public class Cart
    {
        [Key]
        public string ID { get; set; }
        public List<CartItem> CartItems { get; set; }//Collection navigation property
    }
}
