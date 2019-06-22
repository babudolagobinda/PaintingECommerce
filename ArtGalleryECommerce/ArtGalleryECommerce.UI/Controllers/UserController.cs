using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArtGalleryECommerce.Model.AdminDTO;
using ArtGalleryECommerce.Model.UserDTO;
using ArtGalleryECommerce.Dal.Repository;
using ArtGalleryECommerce.Dal.Admin;
using ArtGalleryECommerce.Dal.User;
using ArtGalleryECommerce.UI.Models;

namespace ArtGalleryECommerce.UI.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            ItemGroupDal itemGroupDal = new ItemGroupDal();
            UserHomeDal userHomeDal = new UserHomeDal();
            List<ItemGroupDto> lstItemGroupDto = itemGroupDal.GetAndEditItemGroup(0, 1);
            List<ProductListDto> lstProductListDto = userHomeDal.GetTopProductList();
            ViewBag.ItemGroups = lstItemGroupDto;
            ViewBag.LatestProduct = lstProductListDto;
            return View();
        }
        public ActionResult ProductList()
        {
            ItemGroupDal itemGroupDal = new ItemGroupDal();
            List<ItemGroupDto> lstItemGroupDto = itemGroupDal.GetAndEditItemGroup(0, 1);
            ViewBag.ItemGroups = lstItemGroupDto;
            return View();
        }
       
    }
}