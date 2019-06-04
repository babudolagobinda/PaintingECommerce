using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using ArtGalleryECommerce.Model.DTO;
using ArtGalleryECommerce.Model.Repository;
using ArtGalleryECommerce.UI.Models;
using System.Web.Security;


namespace ArtGalleryECommerce.UI.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(AdminLoginModel adminLoginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(adminLoginModel);
            }
            AdminLoginDto user = new AdminLoginDto() { UserName = adminLoginModel.UserName, Password = adminLoginModel.Password };
            user = AdminAuthenticationRepository.GetUserDetails(user);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(adminLoginModel.UserName, false);
                var authTicket = new FormsAuthenticationTicket(1, user.UserName, DateTime.Now, DateTime.Now.AddMinutes(20), false, user.Roles);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);
                return RedirectToAction("DashBoard", "Admin");
            }

            else
            {
                ViewBag.LoginError = "Invalid Login Attempt";
                return View();
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult DashBoard()
        {
            return View();
        }
        public ActionResult LogOut()
        {

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Admin");
        }
        public ActionResult ItemGroup()
        {
            return View();
        }
    }
}