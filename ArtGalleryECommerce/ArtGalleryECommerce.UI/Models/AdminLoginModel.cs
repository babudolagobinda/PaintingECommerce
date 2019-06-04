using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ArtGalleryECommerce.UI.Models
{
    public class AdminLoginModel
    {

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please Enter UserName"), MaxLength(10)]
        public string UserName { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please Enter Password"), MaxLength(10)]
        public string Password { get; set; }

    }
}