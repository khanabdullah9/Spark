using Microsoft.AspNetCore.Mvc.Rendering;
using Spark.Data;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Spark.ViewModels
{
    public class AssignClaimViewModel
    {
        public AssignClaimViewModel() 
        {
            Claim = new List<SelectListItem>();
        }
        [Required(ErrorMessage ="Enter the email of the user")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "claim(lower) is needed")]
        public string claim { get; set; }
        [Required(ErrorMessage = "Claim(Upper) is needed")]
        public List<SelectListItem> Claim { get; set; }
    }
}
