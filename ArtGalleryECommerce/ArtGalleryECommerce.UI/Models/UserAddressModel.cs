using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ArtGalleryECommerce.UI.Models
{
    public class UserAddressModel
    {
        public int? AddressId { get; set; }
        public int? UserId { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please Enter Name"), MaxLength(40)]
        public string Name { get; set; }
        [Required(ErrorMessage = "You must provide a phone number")]
        [Display(Name = "Home Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter PhoneNumber as 0123456789, 012-345-6789, (012)-345-6789.")]
        public string MobileNo { get; set; }
        [DataType(DataType.PostalCode)]
        [Required(ErrorMessage = "Please Enter Pincode"), MaxLength(10)]
        public string Pincode { get; set; }
        [DataType(DataType.PostalCode)]
        [Required(ErrorMessage = "Please Enter Address"), MaxLength(200)]
        public string Address { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please Enter Locality"), MaxLength(40)]
        public string Locality { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please Enter City"), MaxLength(40)]
        public string City { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please Enter State"), MaxLength(40)]
        public string State { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please Enter Country"), MaxLength(40)]
        public string Country { get; set; }
        public string CreatedDate { get; set; }
        public int IsActive { get; set; }
    }
}