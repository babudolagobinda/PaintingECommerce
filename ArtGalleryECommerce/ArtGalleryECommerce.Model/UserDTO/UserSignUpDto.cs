using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGalleryECommerce.Model.UserDTO
{
    public class UserSignUpDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string MobileNo { get; set; }
        public string Gender { get; set; }
        public string CreatedDate { get; set; }
        public int IsActive { get; set; }
    }
}
