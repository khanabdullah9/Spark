using System.ComponentModel.DataAnnotations;

namespace Spark.Models
{//Dependendent Entity
    public class CartItem
    {   
        [Key]
        public string ID { get; set; }
        public string productID { get; set; }//foreing key
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public string CartId { get; set; }//foreign key property
        public Cart Cart { get; set; }//inverse navigation property (Cart)
    }
}
