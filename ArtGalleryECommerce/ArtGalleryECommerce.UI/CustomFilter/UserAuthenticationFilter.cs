using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace ArtGalleryECommerce.UI.CustomFilter
{
    public class UserAuthenticationFilter : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["AdminId"])))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
            else
            {
                var adminId = Convert.ToString(filterContext.HttpContext.Session["AdminId"]);
                var viewBag = filterContext.Controller.ViewBag;
                viewBag.CustomValue = adminId;
            }
        }
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "Index"
                };
            }
        }
    }
}