using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ArtGalleryECommerce.UI.Models
{
    public class UserLoginModel
    {
        //public int UserId { get; set; }
        //[DataType(DataType.Text)]
        //[Required(ErrorMessage = "Please Enter Name"), MaxLength(40)]
        //public string Name { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please Enter EmailId")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        //[Required(ErrorMessage = "Confirm Password is required")]
        //[StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        //[DataType(DataType.Password)]
        //[Compare("Password")]
        //public string ConfirmPassword { get; set; }
        //[Required(ErrorMessage = "You must provide a phone number")]
        //[Display(Name = "Home Phone")]
        //[DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        //public string MobileNo { get; set; }
        //[Required(ErrorMessage = "Please Select Gender.")]
        //public string Gender { get; set; }
        //public string CreatedDate { get; set; }
        //public int IsActive { get; set; }
    }
}