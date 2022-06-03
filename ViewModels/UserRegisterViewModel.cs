using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Spark.ViewModels
{
    public class UserRegisterViewModel
    {
        public UserRegisterViewModel() 
        {
            State = new List<SelectListItem>();
        }
        [Required(ErrorMessage ="First name is needed")]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is needed")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is needed")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Address is needed")]
        public string Address { get; set; }
        [Required(ErrorMessage = "City is needed")]
        public string City { get; set; }
        public string state { get; set; }
        [Required(ErrorMessage = "State is needed")]
        public List<SelectListItem> State { get; set; }
        [Required(ErrorMessage = "Pin code is needed")]
        [Display(Name = "Pin Code")]
        public string PinCode { get; set; }
        [Required(ErrorMessage = "Enter a strong password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Re enter password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password and Confirm Password must match!")]
        public string Confirm_Password { get; set; }
    }
}
