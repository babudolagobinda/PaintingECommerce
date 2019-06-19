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
using ArtGalleryECommerce.Dal.Admin;
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
                Session["AdminId"] = admin.AdminId;
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
        public ActionResult GetAllItemGroup()
        {
            ItemGroupDal itemGroupDal = new ItemGroupDal();
            List<ItemGroupDto> lstItemGroupDto = itemGroupDal.GetAndEditItemGroup(0, 1);
            return Json(lstItemGroupDto, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveItemGroup()
        {
            try
            {
                ItemGroupDal itemGroupDal = new ItemGroupDal();
                ItemGroupDto itemGroupDto = new ItemGroupDto();
                itemGroupDto.GroupId = Convert.ToInt32(System.Web.HttpContext.Current.Request["GroupId"] == "" ? "0" : System.Web.HttpContext.Current.Request["GroupId"]);
                string Message, fileName, actualFileName;
                Message = fileName = actualFileName = string.Empty;
                if (Request.Files.Count > 0)
                {
                    var fileContent = Request.Files[0];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        actualFileName = fileContent.FileName;
                        fileName = Guid.NewGuid() + Path.GetExtension(fileContent.FileName);
                        itemGroupDto.GroupImage = fileName;
                    }
                    fileContent.SaveAs(Path.Combine(Server.MapPath("~/UploadImages/"), fileName));
                }
                else
                {
                    fileName = "";
                    if (itemGroupDto.GroupId > 0)
                    {
                        dynamic imgFile = itemGroupDal.GetAndEditItemGroup(itemGroupDto.GroupId, 1);
                        itemGroupDto.GroupImage = Convert.ToString(imgFile[0].GroupImage);
                    }
                    else
                    {
                        itemGroupDto.GroupImage = fileName;
                    }
                }
                itemGroupDto.GroupName = System.Web.HttpContext.Current.Request["GroupName"];
                itemGroupDto.GroupDesc = System.Web.HttpContext.Current.Request["GroupDesc"];
                itemGroupDto.CreatedBy = Convert.ToInt32(Session["AdminId"]);
                itemGroupDto.ModifiedBy = Convert.ToInt32(Session["AdminId"]);
                itemGroupDto.IsActive = 1;

                int i = itemGroupDal.SaveItemGroup(itemGroupDto);
                return Json(i, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult DeleteItemGroup(int GroupId)
        {
            ItemGroupDal itemGroupDal = new ItemGroupDal();
            int i = itemGroupDal.DeleteItemGroup(GroupId);
            return Json(i, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ItemCategory()
        {
            ViewBag.AdminDetails = TempData["AdminDetails"];
            TempData.Keep();
            return View();
        }
        [HttpPost]
        public ActionResult GetAllItemCategory()
        {
            ItemCategoryDal itemCategoryDal = new ItemCategoryDal();
            List<ItemCategoryDto> lstItemCategoryDto = itemCategoryDal.GetAndEditItemCategory(0, 1);
            return Json(lstItemCategoryDto, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetItemCategoryByGroupId(int GroupId)
        {
            ItemCategoryDal itemCategoryDal = new ItemCategoryDal();
            List<ItemCategoryDto> lstItemCategoryDto = itemCategoryDal.GetItemCategoryByGroupId(GroupId);
            return Json(lstItemCategoryDto, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveItemCategory()
        {
            try
            {
                ItemCategoryDal itemCategoryDal = new ItemCategoryDal();
                ItemCategoryDto itemCategoryDto = new ItemCategoryDto();
                itemCategoryDto.CategoryId = Convert.ToInt32(System.Web.HttpContext.Current.Request["CategoryId"] == "" ? "0" : System.Web.HttpContext.Current.Request["CategoryId"]);
                string Message, fileName, actualFileName;
                Message = fileName = actualFileName = string.Empty;
                if (Request.Files.Count > 0)
                {
                    var fileContent = Request.Files[0];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        actualFileName = fileContent.FileName;
                        fileName = Guid.NewGuid() + Path.GetExtension(fileContent.FileName);
                        itemCategoryDto.CategoryImage = fileName;
                    }
                    fileContent.SaveAs(Path.Combine(Server.MapPath("~/UploadImages/"), fileName));
                }
                else
                {
                    fileName = "";
                    if (itemCategoryDto.CategoryId > 0)
                    {
                        dynamic imgFile = itemCategoryDal.GetAndEditItemCategory(itemCategoryDto.CategoryId, 1);
                        itemCategoryDto.CategoryImage = Convert.ToString(imgFile[0].CategoryImage);
                    }
                    else
                    {
                        itemCategoryDto.CategoryImage = fileName;
                    }
                }
                itemCategoryDto.GroupId =Convert.ToInt32(System.Web.HttpContext.Current.Request["GroupId"]);
                itemCategoryDto.CategoryName = System.Web.HttpContext.Current.Request["CategoryName"];
                itemCategoryDto.CategoryDesc = System.Web.HttpContext.Current.Request["CategoryDesc"];
                itemCategoryDto.CreatedBy = Convert.ToInt32(Session["AdminId"]);
                itemCategoryDto.ModifiedBy = Convert.ToInt32(Session["AdminId"]);
                itemCategoryDto.IsActive = 1;

                int i = itemCategoryDal.SaveItemCategory(itemCategoryDto);
                return Json(i, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public ActionResult DeleteItemCategory(int CategoryId)
        {
            ItemCategoryDal itemCategoryDal = new ItemCategoryDal();
            int i = itemCategoryDal.DeleteItemCategory(CategoryId);
            return Json(i, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ItemMaster()
        {
            ViewBag.AdminDetails = TempData["AdminDetails"];
            TempData.Keep();
            return View();
        }
        [HttpPost]
        public ActionResult GetAllItemMaster()
        {
            ItemMasterDal itemMasterDal = new ItemMasterDal();
            List<ItemMasterDto> lstItemMasterDto = itemMasterDal.GetAndEditItemMaster(0, 1);
            return Json(lstItemMasterDto, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveItemMaster()
        {
            try
            {
                ItemMasterDal itemMasterDal = new ItemMasterDal();
                ItemMasterDto itemMasterDto = new ItemMasterDto();
                itemMasterDto.ItemId = Convert.ToInt32(System.Web.HttpContext.Current.Request["ItemId"] == "" ? "0" : System.Web.HttpContext.Current.Request["ItemId"]);
                string Message, fileName, actualFileName;
                Message = fileName = actualFileName = string.Empty;
                if (Request.Files.Count > 0)
                {
                    var fileContent = Request.Files[0];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        actualFileName = fileContent.FileName;
                        fileName = Guid.NewGuid() + Path.GetExtension(fileContent.FileName);
                        itemMasterDto.ItemImage = fileName;
                    }
                    fileContent.SaveAs(Path.Combine(Server.MapPath("~/UploadImages/"), fileName));
                }
                else
                {
                    fileName = "";
                    if (itemMasterDto.ItemId > 0)
                    {
                        dynamic imgFile = itemMasterDal.GetAndEditItemMaster(itemMasterDto.ItemId, 1);
                        itemMasterDto.ItemImage = Convert.ToString(imgFile[0].ItemImage);
                    }
                    else
                    {
                        itemMasterDto.ItemImage = fileName;
                    }
                }
                itemMasterDto.GroupId = Convert.ToInt32(System.Web.HttpContext.Current.Request["GroupId"]);
                itemMasterDto.CategoryId = Convert.ToInt32(System.Web.HttpContext.Current.Request["CategoryId"]);
                itemMasterDto.ItemName = System.Web.HttpContext.Current.Request["ItemName"];
                itemMasterDto.ItemDesc = System.Web.HttpContext.Current.Request["ItemDesc"];
                itemMasterDto.CreatedBy = Convert.ToInt32(Session["AdminId"]);
                itemMasterDto.ModifiedBy = Convert.ToInt32(Session["AdminId"]);
                itemMasterDto.IsActive = 1;

                int i = itemMasterDal.SaveItemMaster(itemMasterDto);
                return Json(i, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult DeleteItemMaster(int ItemId)
        {
            ItemMasterDal itemMasterDal = new ItemMasterDal();
            int i = itemMasterDal.DeleteItemMaster(ItemId);
            return Json(i, JsonRequestBehavior.AllowGet);
        }
    }
}