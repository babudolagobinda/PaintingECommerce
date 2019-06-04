using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtGalleryECommerce.Model.DTO;

namespace ArtGalleryECommerce.Model.Repository
{
    public class AdminAuthenticationRepository
    {
        static List<AdminLoginDto> users = new List<AdminLoginDto>() {
        new AdminLoginDto() {UserName="abcd",Roles="Admin",Password="abcadmin" },
         new AdminLoginDto() {UserName="abcde",Roles="Admin,User",Password="abcadmin" },
         new AdminLoginDto() {UserName="abcdef",Roles="Edior",Password="abcadmin" }
    };

        public static AdminLoginDto GetUserDetails(AdminLoginDto user)
        {
            return users.Where(u => u.UserName.ToLower() == user.UserName.ToLower() &&
            u.Password == user.Password).FirstOrDefault();
        }
    }
}
