using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtGalleryECommerce.Model.AdminDTO;
using ArtGalleryECommerce.Dal.Admin;

namespace ArtGalleryECommerce.Dal.Repository
{
   public class AdminAuthenticationRepository
    {
        public static AdminLoginDto GetAdminDetails(AdminLoginDto admin)
        {
            AdminLoginDal adminLoginDal = new AdminLoginDal();
            AdminLoginDto adminDetails= adminLoginDal.GetAdminLogin(admin);
            return adminDetails;
        }
        // static AdminLoginDto users = GetAdminDetails(AdminLoginDto admin);
        //public static AdminLoginDto GetUserDetails(AdminLoginDto adminDetails)
        //{
        //    return users.Where(u => u.UserName.ToLower() == adminDetails.UserName.ToLower() &&
        //    u.Password == adminDetails.Password).FirstOrDefault();
        //}
    }
}
