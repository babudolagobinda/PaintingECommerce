using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using ArtGalleryECommerce.Model.AdminDTO;
using ArtGalleryECommerce.Dal.Repository;
using ArtGalleryECommerce.UI.Models;
using System.Web.Security;
using System.IO;


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
            AdminLoginDto admin = new AdminLoginDto() { UserName = adminLoginModel.UserName, Password = adminLoginModel.Password };
            admin = AdminAuthenticationRepository.GetAdminDetails(admin);
            if (admin.AdminId > 0)
            {
                FormsAuthentication.SetAuthCookie(adminLoginModel.UserName, false);
                var authTicket = new FormsAuthenticationTicket(1, admin.UserName, DateTime.Now, DateTime.Now.AddMinutes(20), false, admin.UserRole);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);
                TempData["AdminDetails"] = admin;
                TempData.Keep();
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
            ViewBag.AdminDetails = TempData["AdminDetails"];
            TempData.Keep();
            return View();
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Admin");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ItemGroup()
        {
            ViewBag.AdminDetails = TempData["AdminDetails"];
            TempData.Keep();
            return View();
        }
        [HttpPost]
        public ContentResult SaveItemGroup(FormCollection formCollection)
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        var fileName = Guid.NewGuid() + Path.GetFileName(fname);
                        var filePath = Path.Combine(Server.MapPath("../UploadImages/"), fileName);
                        string fl = filePath.Substring(filePath.LastIndexOf("\\"));
                        string[] split = fl.Split('\\');
                        string newpath = split[1];
                        string imagepath = "~/UploadImages/" + newpath;
                        file.SaveAs(filePath);
                        ItemGroupDto itemGroupDto = new ItemGroupDto();
                        itemGroupDto.GroupName = formCollection["GroupName"];
                        itemGroupDto.GroupDesc = formCollection["GroupDesc"];
                        itemGroupDto.GroupImage = imagepath;
                       
                    }
                    return Content("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Content("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Content("No files selected.");
            }
        }
        //public ActionResult ItemGroup(FormCollection form)
        //{
        //    if(!ModelState.IsValid)
        //    {
        //        ItemGroupDto itemGroupDto = new ItemGroupDto();
        //        itemGroupDto.GroupName = form["GroupName"];
        //        itemGroupDto.GroupDesc = form["GroupDesc"];
        //        if (Request.Files.Count > 0)
        //        {
        //            var docFiles = new List<string>();
        //            foreach (string file in Request.Files)
        //            {
        //                var postedFile = Request.Files[file];
        //                var fileName = Guid.NewGuid() + Path.GetFileName(postedFile.FileName);
        //                var filePath = Path.Combine(Server.MapPath("../UploadImages/"), fileName);
        //                postedFile.SaveAs(filePath);
        //                string fl = filePath.Substring(filePath.LastIndexOf("\\"));
        //                string[] split = fl.Split('\\');
        //                string newpath = split[1];
        //                string imagepath = "~/UploadImages/" + newpath;
        //                docFiles.Add(filePath);
        //            }
        //        }
        //        else
        //        {
        //            // else code whatever you want
        //        }
        //    }

        //    return View();
        //}
    }
}