using System;
using System.Net;
using System.Collections.Generic;
using System.Configuration;
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
using System.Security.Cryptography;
using System.Text;

namespace ArtGalleryECommerce.UI.Controllers
{
    public class UserController : Controller
    {
        ItemGroupDal itemGroupDal = new ItemGroupDal();
        MainBannerDal mainBannerDal = new MainBannerDal();
        UserHomeDal userHomeDal = new UserHomeDal();
        UserSignUpDal userSignUpDal = new UserSignUpDal();
        UserReviewDal userReviewDal = new UserReviewDal();
        UserOrderDetailsDal userOrderDetailsDal = new UserOrderDetailsDal();
        public string action1 = string.Empty;
        public string hash1 = string.Empty;
        public string txnid1 = string.Empty;
        // GET: User
        public ActionResult Index()
        {
            List<ProductListDto> lstProductListDto = new List<ProductListDto>();
            List<ProductListDto> lstBottomProductListDto = new List<ProductListDto>();
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
            List<ProductListDto> lstBottomProductListDto = new List<ProductListDto>();
            lstBottomProductListDto = userHomeDal.GetBottomProductList();
            List<UserReviewDto> lstUserReviewDto = userReviewDal.GetAllReviewList(itemId);
            ViewBag.OldestProduct = lstBottomProductListDto;
            ViewBag.ItemGroups = lstItemGroupDto;
            ViewBag.ItemDetails = lstProductListDto;
            ViewBag.ReviewList = lstUserReviewDto;
            ViewBag.AverageRatingPerItem = userReviewDal.GetAverageRatingPerItem(itemId);
            ViewBag.TotalCountRatingPerItem = userReviewDal.GetTotalCountRatingPerItem(itemId);
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
            UserHomeDal userHomeDal = new UserHomeDal();
            dynamic totalValue = TempData["TotalPricetemp"];
            //dynamic totalValue = Session["TotalPrice"];
            //dynamic currencySymbol = Session["currency"].ToString();
            decimal totalPriceValue = userHomeDal.CurrencyConverter(totalValue);
            UserAddressDal userAddressDal = new UserAddressDal();
            List<ItemGroupDto> lstItemGroupDto = itemGroupDal.GetAndEditItemGroup(0, 1);
            ViewBag.ItemGroups = lstItemGroupDto;
            dynamic userId = Session["UserId"];
            List<UserAddressModel> userAddressModel = GetAllUserAddress(userId);
            TempData["UserAddress"] = userAddressModel;
            TempData["TotalPrice"] = totalValue;
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
        public void UserCheckOutPayment(FormCollection form)
        {
            var addressId = Convert.ToInt32(form[12]);
            dynamic userId = Session["UserId"];
            UserAddressDal userAddressDal = new UserAddressDal();
            UserAddressDto userAddressDto = userAddressDal.GetUserAddressByAddressId(addressId, userId, 1);
            UserSignUpDto userSignUpDto = userSignUpDal.GetUserDetailsByUserId(userId);
            string firstName = userAddressDto.Name;
            string amount = form["totalPrice"];
            string productInfo = "Product";
            string email = userSignUpDto.EmailId;
            string phone = userAddressDto.MobileNo;
            //string surl = "http://localhost:64535/User/ThankYou";
            //string furl = "http://localhost:64535/User/Failure";
            string surl = "https://chandnicreativeart.com/User/ThankYou";
            string furl = "https://chandnicreativeart.com/User/Failure";

            RemotePost myremotepost = new RemotePost();
            string key = "StLG15HX";
            string salt = "lJxnsim0Zz";

            //posting all the parameters required for integration.

            myremotepost.Url = "https://secure.payu.in/_payment";
            myremotepost.Add("key", key);
            string txnid = Generatetxnid();
            myremotepost.Add("txnid", txnid);
            myremotepost.Add("amount", amount);
            myremotepost.Add("productinfo", productInfo);
            myremotepost.Add("firstname", firstName);
            myremotepost.Add("phone", phone);
            myremotepost.Add("email", email);
            myremotepost.Add("surl", surl);//Change the success url here depending upon the port number of your local system.
            myremotepost.Add("furl", furl);//Change the failure url here depending upon the port number of your local system.
            myremotepost.Add("service_provider", "payu_paisa");
            //string hashString = key + "|" + txnid + "|" + amount + "|" + productInfo + "|" + firstName + "|" + email + "|" + phone + "|" + surl + "|" + furl + "|" + salt;
            string hashString = key + "|" + txnid + "|" + amount + "|" + productInfo + "|" + firstName + "|" + email + "|||||||||||" + salt;
            //string hashString = "3Q5c3q|2590640|3053.00|OnlineBooking|vimallad|ladvimal@gmail.com|||||||||||mE2RxRwx";
            string hash = Generatehash512(hashString);
            myremotepost.Add("hash", hash);

            myremotepost.Post();
        }
        
        public class RemotePost
        {
            private System.Collections.Specialized.NameValueCollection Inputs = new System.Collections.Specialized.NameValueCollection();
            public string Url = "";
            public string Method = "post";
            public string FormName = "form1";
            public void Add(string name, string value)
            {
                Inputs.Add(name, value);
            }
            public void Post()
            {
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.Write("<html><head>");
                System.Web.HttpContext.Current.Response.Write(string.Format("</head><body onload=\"document.{0}.submit()\">", FormName));
                System.Web.HttpContext.Current.Response.Write(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", FormName, Method, Url));
                for (int i = 0; i < Inputs.Keys.Count; i++)
                {
                    System.Web.HttpContext.Current.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", Inputs.Keys[i], Inputs[Inputs.Keys[i]]));
                }
                System.Web.HttpContext.Current.Response.Write("</form>");
                System.Web.HttpContext.Current.Response.Write("</body></html>");
                System.Web.HttpContext.Current.Response.End();
            }
        }
        //Hash generation Algorithm
        public string Generatehash512(string text)
        {

            byte[] message = Encoding.UTF8.GetBytes(text);
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            SHA512Managed hashString = new SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }
        public string Generatetxnid()
        {
            Random rnd = new Random();
            string strHash = Generatehash512(rnd.ToString() + DateTime.Now);
            string txnid1 = strHash.ToString().Substring(0, 20);
            return txnid1;
        }

        [HttpPost]
        public ActionResult EditUserAddress(int addressId)
        {
            dynamic userId = Session["UserId"];
            UserAddressDal userAddressDal = new UserAddressDal();
            UserAddressDto userAddressDto = userAddressDal.GetUserAddressByAddressId(addressId, userId, 1);
            return Json(userAddressDto, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteUserAddress(int addressId)
        {
            dynamic userId = Session["UserId"];
            UserAddressDal userAddressDal = new UserAddressDal();
            int i = userAddressDal.DeleteUserddressByUserId(userId, addressId);
            return Json(i, JsonRequestBehavior.AllowGet);
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
                    Session["Email"] = userSignUpDto.EmailId;
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
        [UserAuthenticationFilterForWebsite]
        public ActionResult MyOrders()
        {
            List<ItemGroupDto> lstItemGroupDto = itemGroupDal.GetAndEditItemGroup(0, 1);
            ViewBag.ItemGroups = lstItemGroupDto;
            int userId = Convert.ToInt32(Session["UserId"].ToString());
            List<MyOrdersDetailsResponseDto> lstOrderNoByUserId = userOrderDetailsDal.GetOrderHistoryByUserId(userId);
            //List<MyOrdersDetailsResponseDto> lstOfOrderDetailsByOrderNo = new List<MyOrdersDetailsResponseDto>();
            //List<List<MyOrdersDetailsResponseDto>> lstOfLstOfOrderDetailsByOrderNo = new List<List<MyOrdersDetailsResponseDto>>();
            //for (int i = 0; i < lstOrderNoByUserId.Count; i++)
            //{
            //    lstOfOrderDetailsByOrderNo = userOrderDetailsDal.GetAllOrderDetailsByOrderNo(userId, lstOrderNoByUserId[i].OrderNumber);
            //    lstOfLstOfOrderDetailsByOrderNo.Add(lstOfOrderDetailsByOrderNo);
            //}
            ViewBag.ListOfOrderNo = lstOrderNoByUserId;
            //ViewBag.ListOfOrderDetailsByOrderNo = lstOfLstOfOrderDetailsByOrderNo;
            return View();
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
        public ActionResult CurrencyPriceConverter(string currency, string prevCurrency)
        {
            Session["currency"] = currency;
            Session["PrevCurrency"] = prevCurrency;
            var curr = Session["currency"].ToString();
            var prevCurr = Session["PrevCurrency"].ToString();
            List<string> currencyList = new List<string>();
            currencyList.Add(curr);
            currencyList.Add(prevCurr);

            //TempData["currency"] = currency;
            //TempData.Keep();
            //var curr = TempData["currency"];
            return Json(currencyList, JsonRequestBehavior.AllowGet);
        }
        [UserAuthenticationFilterForWebsite]
        public ActionResult WishList()
        {
            List<ItemGroupDto> lstItemGroupDto = itemGroupDal.GetAndEditItemGroup(0, 1);
            ViewBag.ItemGroups = lstItemGroupDto;
            return View();
        }
        [HttpPost]
        public ActionResult WishList(dynamic ItemId)
        {
            List<ItemGroupDto> lstItemGroupDto = itemGroupDal.GetAndEditItemGroup(0, 1);
            List<ProductListDto> lstProductListDto = new List<ProductListDto>();
            foreach (var item in ItemId)
            {
                if (item != "0")
                {
                    ProductListDto ProductListDto = userHomeDal.GetProductListByItemId(int.Parse(item));
                    lstProductListDto.Add(ProductListDto);
                }
            }
            ViewBag.ItemGroups = lstItemGroupDto;
            return PartialView("_PartialMyWishList", lstProductListDto);
        }
        [HttpPost]
        public ActionResult AddToWishList(int ItemId)
        {
            if (Session["UserId"] == null)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(ItemId, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        [UserAuthenticationFilterForWebsite]
        public ActionResult SaveUserReview(string review, string rating, int itemId)
        {
            UserReviewDal userReviewDal = new UserReviewDal();
            UserReviewDto userReviewDto = new UserReviewDto();
            userReviewDto.UserId = Convert.ToInt32(Session["UserId"]);
            userReviewDto.ItemId = itemId;
            userReviewDto.Review = review;
            userReviewDto.Rating = rating;
            userReviewDto.IsActive = 1;
            int i = userReviewDal.SaveUsersReview(userReviewDto);
            return Json(i, JsonRequestBehavior.AllowGet);
        }
        [Route("SaveUserOrderDetails")]
        [HttpPost]
        public ActionResult SaveUserOrderDetails(dynamic ItemId, int AddressId, string CurrencyType, decimal PaidPrice, string PaymentType)
        {
            UserOrderDetailsDto userOrderDetailsDto = new UserOrderDetailsDto();
            userOrderDetailsDto.OrderNumber = GenerateOrderNumber();
            int i = 0;
            foreach (var item in ItemId)
            {

                userOrderDetailsDto.UserId = Convert.ToInt32(Session["UserId"]);
                userOrderDetailsDto.ItemId = Convert.ToInt32(item);
                userOrderDetailsDto.AddressId = AddressId;
                ProductListDto productListDto = userHomeDal.GetProductListByItemId(Convert.ToInt32(item));
                userOrderDetailsDto.Mrp = productListDto.Mrp;
                userOrderDetailsDto.Discount = productListDto.Discount;
                userOrderDetailsDto.Price = productListDto.Price;
                userOrderDetailsDto.CurrencyType = CurrencyType;
                userOrderDetailsDto.Quantity = 1;
                userOrderDetailsDto.TotalAmount = PaidPrice;
                userOrderDetailsDto.PaymentType = PaymentType;
                userOrderDetailsDto.Status = 0;
                i = userOrderDetailsDal.SaveOrderDetails(userOrderDetailsDto);
            }
            return Json(i, JsonRequestBehavior.AllowGet);
        }
        private string GenerateOrderNumber()
        {
            string OrderNumber;
            //JI-XXXXXXXXX-XXXX
            Random rnd = new Random();
            long orderPart1 = rnd.Next(100000, 9999999);
            int orderPart2 = rnd.Next(1000, 9999);
            OrderNumber = "OD" + orderPart1 + orderPart2 + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year;
            return OrderNumber;
        }
        [UserAuthenticationFilterForWebsite]
        public ActionResult ThankYou()
        {
            return View();
        }
        public ActionResult ContactUs()
        {
            List<ItemGroupDto> lstItemGroupDto = itemGroupDal.GetAndEditItemGroup(0, 1);
            ViewBag.ItemGroups = lstItemGroupDto;
            return View();
        }
        public ActionResult PrivacyPolicy()
        {
            List<ItemGroupDto> lstItemGroupDto = itemGroupDal.GetAndEditItemGroup(0, 1);
            ViewBag.ItemGroups = lstItemGroupDto;
            return View();
        }
        public ActionResult AboutMe()
        {
            List<ItemGroupDto> lstItemGroupDto = itemGroupDal.GetAndEditItemGroup(0, 1);
            ViewBag.ItemGroups = lstItemGroupDto;
            return View();
        }
        public ActionResult TermsAndConditions()
        {
            List<ItemGroupDto> lstItemGroupDto = itemGroupDal.GetAndEditItemGroup(0, 1);
            ViewBag.ItemGroups = lstItemGroupDto;
            return View();
        }
        public ActionResult Failure()
        {
            return View();
        }
    }
}
