using System.ComponentModel.DataAnnotations;
using Spark.Data;

namespace Spark.Models
{
    public class Checkout
    {
        [Key]
        public string ID { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string CartId { get; set; }
        public Cart Cart { get; set; }
        public List<Product> products { get; set; }

    }
}
