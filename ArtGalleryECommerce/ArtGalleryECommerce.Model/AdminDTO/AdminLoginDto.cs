using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGalleryECommerce.Model.AdminDTO
{
    public class AdminLoginDto
    { 
        public int AdminId { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string ProfPic { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public string Password { get; set; }
        public int IsActive { get; set; }
    }
}
