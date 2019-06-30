using System;
using System.Net;
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
using System.Web.Script.Serialization;
using System.Web.UI;
using ArtGalleryECommerce.UI.CustomFilter;

namespace ArtGalleryECommerce.UI.Controllers
{
    public class UserController : Controller
    {
        ItemGroupDal itemGroupDal = new ItemGroupDal();
        MainBannerDal mainBannerDal = new MainBannerDal();
        UserHomeDal userHomeDal = new UserHomeDal();
        // GET: User
        public ActionResult Index()
        {
            List<ItemGroupDto> lstItemGroupDto = itemGroupDal.GetAndEditItemGroup(0, 1);
            List<ProductListDto> lstProductListDto = userHomeDal.GetTopProductList();
            List<MainBannerDto> lstMainBannerDto = mainBannerDal.GetAllMainBanner(0, 1);
            ViewBag.ItemGroups = lstItemGroupDto;
            ViewBag.LatestProduct = lstProductListDto;
            ViewBag.MainBanners = lstMainBannerDto;
            return View();
        }
        public ActionResult ProductList(int groupId, int categoryId)
        {
            List<ProductListDto> lstProductListDto = new List<ProductListDto>();
            List<ItemGroupDto> lstItemGroupDto = itemGroupDal.GetAndEditItemGroup(0, 1);
            ViewBag.ItemGroups = lstItemGroupDto;
            if (categoryId > 0)
            {
                lstProductListDto = userHomeDal.GetProductListByCategoryId(categoryId);
            }
            else
            {
                lstProductListDto = userHomeDal.GetProductListByGroupId(groupId);
            }
            ViewBag.ProductList = lstProductListDto;
            return View("ProductList", lstProductListDto);
        }
        [HttpPost]
        public ActionResult ProductList(int[] lstFilter)
        {
            List<ProductListDto> lstProductListDto = new List<ProductListDto>();
            List<ItemGroupDto> lstItemGroupDto = itemGroupDal.GetAndEditItemGroup(0, 1);
            ViewBag.ItemGroups = lstItemGroupDto;
            if (lstFilter != null)
            {
                foreach (var groupId in lstFilter)
                {
                    lstProductListDto = userHomeDal.GetProductListByGroupId(groupId);
                }
            }
            return PartialView("_PartialProductList", lstProductListDto);
        }
        public ActionResult ProductDetails(int itemId)
        {
            ProductListDto lstProductListDto = userHomeDal.GetProductListByItemId(itemId);
            List<ItemGroupDto> lstItemGroupDto = itemGroupDal.GetAndEditItemGroup(0, 1);
            ViewBag.ItemGroups = lstItemGroupDto;
            ViewBag.ItemDetails = lstProductListDto;
            return View();
        }
        public ActionResult MyCart()
        {
            List<ItemGroupDto> lstItemGroupDto = itemGroupDal.GetAndEditItemGroup(0, 1);

            ViewBag.ItemGroups = lstItemGroupDto;
            return View();
        }
        [HttpPost]
        public ActionResult MyCart(dynamic cartItems)
        {
            List<ProductListDto> lstProductListDto = new List<ProductListDto>();
            List<ItemGroupDto> lstItemGroupDto = itemGroupDal.GetAndEditItemGroup(0, 1);
            foreach (var item in cartItems)
            {
                if (item != "0")
                {
                    ProductListDto ProductListDto = userHomeDal.GetProductListByItemId(int.Parse(item));
                    lstProductListDto.Add(ProductListDto);
                }
            }
            ViewBag.ItemGroups = lstItemGroupDto;
            return PartialView("_PartialMyCart", lstProductListDto);
        }
        [UserAuthenticationFilterForWebsite]
        public ActionResult UserCheckOut()
        {
            return View();
        }
        public ActionResult UserLogin()
        {
            return View();
        }
        public void Convert(double price)
        {
            double amount = 0;
            if (double.TryParse(price.ToString(), out amount))
            {
                string url = string.Format("http://rate-exchange.appspot.com/currency?from={0}&to={1}", "INR", "USD");
                WebClient client = new WebClient();
                string rates = client.DownloadString(url);
                Rate rate = new JavaScriptSerializer().Deserialize<Rate>(rates);
                double converted_amount = amount * rate.rate;
                string message = "INR" + ": " + amount + "\\n";
                message += "USD" + ": " + converted_amount + "\\n";
                message += "Rate: 1 " + "INR" + " = " + rate.rate + " " + "USD";
                // ClientScriptManager.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            }
            else
            {
                //ClientScriptManager.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid amount value.');", true);
            }

        }
        public class Rate
        {
            public string to { get; set; }
            public string from { get; set; }
            public double rate { get; set; }
        }
    }
}