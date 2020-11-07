using System.ComponentModel.DataAnnotations;

namespace ArtGalleryECommerce.UI.Models
{
    public class ContactUs
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please Enter EmailId")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string EmailId { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please Enter Subject")]
        public string Subject { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please Enter Message")]
        public string Message { get; set; }
    }
}