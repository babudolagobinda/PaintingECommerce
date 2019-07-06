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
using System.Web.Security;
using ArtGalleryECommerce.UI.CustomFilter;
using AutoMapper;
using System.Text.RegularExpressions;

namespace ArtGalleryECommerce.UI.Controllers
{
    public class UserController : Controller
    {
        ItemGroupDal itemGroupDal = new ItemGroupDal();
        MainBannerDal mainBannerDal = new MainBannerDal();
        UserHomeDal userHomeDal = new UserHomeDal();
        UserSignUpDal userSignUpDal = new UserSignUpDal();
        // GET: User
        public ActionResult Index()
        {
            var currencyValue = Session["currency"];
            List<ProductListDto> lstProductListDto = new List<ProductListDto>();
            List<ProductListDto> lstBottomProductListDto = new List<ProductListDto>();
            //if (currencyValue != null)
            //{
            //    lstProductListDto = userHomeDal.GetTopProductList(currencyValue.ToString());
            //}
            //else
            //{
            //    lstProductListDto = userHomeDal.GetTopProductList();
            //}
            lstProductListDto = userHomeDal.GetTopProductList();
            lstBottomProductListDto = userHomeDal.GetBottomProductList();
            List<ItemGroupDto> lstItemGroupDto = itemGroupDal.GetAndEditItemGroup(0, 1);
            List<MainBannerDto> lstMainBannerDto = mainBannerDal.GetAllMainBanner(0, 1);
            ViewBag.ItemGroups = lstItemGroupDto;
            ViewBag.LatestProduct = lstProductListDto;
            ViewBag.OldestProduct = lstBottomProductListDto;
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
        public List<UserAddressModel> GetAllUserAddress(int userId)
        {
            UserAddressDal userAddressDal = new UserAddressDal();
            List<UserAddressDto> userAddressDto = userAddressDal.GetUserAddressByUserId(0, userId, 1);
            AutoMapper.Mapper.CreateMap<UserAddressDto, UserAddressModel>()
                      .ForMember(o => o.AddressId, b => b.MapFrom(z => z.AddressId))
                      .ForMember(o => o.UserId, b => b.MapFrom(z => z.UserId))
                      .ForMember(o => o.Name, b => b.MapFrom(z => z.Name))
                      .ForMember(o => o.MobileNo, b => b.MapFrom(z => z.MobileNo))
                      .ForMember(o => o.Pincode, b => b.MapFrom(z => z.Pincode))
                      .ForMember(o => o.Address, b => b.MapFrom(z => z.Address))
                      .ForMember(o => o.Locality, b => b.MapFrom(z => z.Locality))
                      .ForMember(o => o.City, b => b.MapFrom(z => z.City))
                      .ForMember(o => o.State, b => b.MapFrom(z => z.State))
                      .ForMember(o => o.Country, b => b.MapFrom(z => z.Country))
                      //.ForMember(o => o.CreatedDate, b => b.MapFrom(z => z.CreatedDate))
                      .ForMember(o => o.IsActive, b => b.MapFrom(z => z.IsActive));
            List<UserAddressModel> userAddressModel = Mapper.Map<List<UserAddressDto>, List<UserAddressModel>>(userAddressDto);
            return userAddressModel;
        }
        [UserAuthenticationFilterForWebsite]
        public ActionResult UserCheckOut()
        {
            UserAddressDal userAddressDal = new UserAddressDal();
            List<ItemGroupDto> lstItemGroupDto = itemGroupDal.GetAndEditItemGroup(0, 1);
            ViewBag.ItemGroups = lstItemGroupDto;
            dynamic userId = Session["UserId"];
            List<UserAddressModel> userAddressModel = GetAllUserAddress(userId);
            TempData["UserAddress"] = userAddressModel;
            TempData.Keep();
            ViewBag.UserAddress = userAddressModel;
            return View();
        }
        [HttpPost]
        [UserAuthenticationFilterForWebsite]
        public ActionResult UserCheckOut(UserAddressModel userAddressModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.UserAddress = GetAllUserAddress(Convert.ToInt32(Session["UserId"].ToString()));
                return View(userAddressModel);
            }
            else
            {
                userAddressModel.UserId = Convert.ToInt32(Session["UserId"].ToString());
                UserAddressDal userAddressDal = new UserAddressDal();
                List<ItemGroupDto> lstItemGroupDto = itemGroupDal.GetAndEditItemGroup(0, 1);
                ViewBag.ItemGroups = lstItemGroupDto;
                UserAddressDto userAddressDto = new UserAddressDto();
                userAddressDto.AddressId = userAddressModel.AddressId;
                userAddressDto.UserId = userAddressModel.UserId;
                userAddressDto.Name = userAddressModel.Name;
                userAddressDto.MobileNo = userAddressModel.MobileNo;
                userAddressDto.Pincode = userAddressModel.Pincode;
                userAddressDto.Address = userAddressModel.Address;
                userAddressDto.Locality = userAddressModel.Locality;
                userAddressDto.City = userAddressModel.City;
                userAddressDto.State = userAddressModel.State;
                userAddressDto.Country = userAddressModel.Country;
                userAddressDto.IsActive = 1;
                int i = userAddressDal.SaveAndUpdateUserAddress(userAddressDto);
                if (i > 0)
                {
                    ViewBag.successText = "Successfully Address Saved";
                }
                else
                {
                    ViewBag.failureText = "Your Address is not saved successfully in our Website. Please Try after Sometime ";
                }
                ModelState.Clear();
                ViewBag.UserAddress = GetAllUserAddress(Convert.ToInt32(Session["UserId"].ToString()));
                return View();
            }
        }

        [HttpPost]
        public ActionResult EditUserAddress(int addressId)
        {
            dynamic userId = Session["UserId"];
            UserAddressDal userAddressDal = new UserAddressDal();
            UserAddressDto userAddressDto = userAddressDal.GetUserAddressByAddressId(addressId, userId, 1);
            return Json(userAddressDto, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UserLogin()
        {

            return View();
        }
        [HttpPost]
        public ActionResult UserLogin(UserLoginModel userLoginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userLoginModel);
            }
            else
            {
                UserSignUpDto userSignUpDto = new UserSignUpDto() { EmailId = userLoginModel.EmailId, Password = userLoginModel.Password };
                userSignUpDto = userSignUpDal.UserSignIn(userSignUpDto);
                if (userSignUpDto.UserId > 0)
                {
                    Session["UserId"] = userSignUpDto.UserId;
                    Session["UserName"] = userSignUpDto.Name;
                    dynamic returnUrl = Session["returnUrl"];
                    if (!String.IsNullOrEmpty(returnUrl))
                        return Redirect(returnUrl);
                    else
                        return RedirectToAction("Index");
                    //dynamic CurrentURL = System.Web.HttpContext.Current.Request.Url;
                    //return RedirectToAction(CurrentURL);
                }
                else
                {
                    ViewBag.LoginError = "Invalid Login Attempt";
                    return View();
                }
            }

        }
        public ActionResult UserRegister()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserRegister(UserSignUpModel userSignUpModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userSignUpModel);
            }
            else
            {
                UserSignUpDto userSignUpDto = new UserSignUpDto();
                userSignUpDto.UserId = userSignUpModel.UserId;
                userSignUpDto.Name = userSignUpModel.Name;
                userSignUpDto.EmailId = userSignUpModel.EmailId;
                userSignUpDto.Password = userSignUpModel.Password;
                userSignUpDto.MobileNo = userSignUpModel.MobileNo;
                userSignUpDto.Gender = userSignUpModel.Gender;
                userSignUpDto.IsActive = 1;
                int i = userSignUpDal.SaveUserDetails(userSignUpDto);
                if (i > 0)
                {
                    ViewBag.successText = "Successfully Registered.Please Login to Your Account";
                }
                else
                {
                    ViewBag.failureText = "You are not Registered. Please Try after Sometime ";
                }
                return View();
            }
        }
        [UserAuthenticationFilterForWebsite]
        public ActionResult MyProfile()
        {
            List<ItemGroupDto> lstItemGroupDto = itemGroupDal.GetAndEditItemGroup(0, 1);
            ViewBag.ItemGroups = lstItemGroupDto;
            dynamic userId = Session["UserId"];
            UserSignUpDto userSignUpDto = userSignUpDal.GetUserDetailsByUserId(userId);
            AutoMapper.Mapper.CreateMap<UserSignUpDto, UserSignUpModel>()
                      .ForMember(o => o.UserId, b => b.MapFrom(z => z.UserId))
                      .ForMember(o => o.Name, b => b.MapFrom(z => z.Name))
                      .ForMember(o => o.EmailId, b => b.MapFrom(z => z.EmailId))
                      .ForMember(o => o.MobileNo, b => b.MapFrom(z => z.MobileNo))
                      .ForMember(o => o.Password, b => b.MapFrom(z => z.Password))
                      .ForMember(o => o.ConfirmPassword, b => b.MapFrom(z => z.ConfirmPassword))
                      .ForMember(o => o.Gender, b => b.MapFrom(z => z.Gender));
            UserSignUpModel userSignUpModel = Mapper.Map<UserSignUpDto, UserSignUpModel>(userSignUpDto);

            // UserSignUpModel userSignUpModel = userSignUpDto.userSignUpDto;
            return View(userSignUpModel);
        }
        [HttpPost]
        public ActionResult MyProfile(UserSignUpModel userSignUpModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userSignUpModel);
            }
            else
            {
                UserSignUpDto userSignUpDto = new UserSignUpDto();
                userSignUpDto.UserId = userSignUpModel.UserId;
                userSignUpDto.Name = userSignUpModel.Name;
                userSignUpDto.EmailId = userSignUpModel.EmailId;
                userSignUpDto.Password = userSignUpModel.Password;
                userSignUpDto.MobileNo = userSignUpModel.MobileNo;
                userSignUpDto.Gender = userSignUpModel.Gender;
                int i = userSignUpDal.SaveUserDetails(userSignUpDto);
                if (i > 0)
                {
                    ViewBag.successText = "Successfully Profile Updated.";
                }
                else
                {
                    ViewBag.failureText = "Your Profile Has Not Updated. Please Try after Sometime ";
                }
                List<ItemGroupDto> lstItemGroupDto = itemGroupDal.GetAndEditItemGroup(0, 1);
                ViewBag.ItemGroups = lstItemGroupDto;
                return View();
            }

        }
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "User");
        }
        public ActionResult CurrencyPriceConverter(string currency)
        {
            Session["currency"] = currency;
            var curr = Session["currency"];
            return Json(curr, JsonRequestBehavior.AllowGet);
        }


    }
}
