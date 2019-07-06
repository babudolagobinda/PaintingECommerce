using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace ArtGalleryECommerce.UI.CustomFilter
{
    public class UserAuthenticationFilterForWebsite : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["UserId"])))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
            else
            {
                var adminId = Convert.ToString(filterContext.HttpContext.Session["UserId"]);
                var viewBag = filterContext.Controller.ViewBag;
                viewBag.CustomValue = adminId;
            }
        }
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                if(HttpContext.Current.Request.UrlReferrer!=null)
                {
                    string returnUrl = HttpContext.Current.Request.UrlReferrer.ToString();
                    filterContext.HttpContext.Session["returnUrl"] = returnUrl;
                }
                
                filterContext.Result = new ViewResult
                {
                    ViewName = "UserLogin"
                };
            }
           
        }
    }
}