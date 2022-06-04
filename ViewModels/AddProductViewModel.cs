using System.ComponentModel.DataAnnotations;

namespace Spark.ViewModels
{
    public class AddProductViewModel
    {
        [Required(ErrorMessage ="Name is needed")]
        [Display(Name ="Name")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Genre is needed")]
        [Display(Name = "Genre")]
        public string Genre { get; set; }
        [Required(ErrorMessage = "Description is needed")]
        [Display(Name = "Description")]
        public string ProductDescription { get; set; }
        [Required(ErrorMessage = "Publisher is needed")]
        [Display(Name = "Publisher")]
        public string Publisher { get; set; }
        [Required(ErrorMessage = "Developer is needed")]
        [Display(Name = "Developer")]
        public string Developer { get; set; }
        [Required(ErrorMessage = "Quantity is needed")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Price is needed")]
        [Display(Name = "Price")]
        public Double Price { get; set; }
        [Required(ErrorMessage = "Product Image is needed")]
        [Display(Name = "Product Image")]
        public IFormFile ProductImage { get; set; }
    }
}
