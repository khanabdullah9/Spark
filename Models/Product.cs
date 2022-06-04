using System.ComponentModel.DataAnnotations;

namespace Spark.Models
{
    public class Product
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public string Developer { get; set; }
        public int Quantity { get; set; }
        public Double Price { get; set; }
        public string product_image_path { get; set; }
    }
}
