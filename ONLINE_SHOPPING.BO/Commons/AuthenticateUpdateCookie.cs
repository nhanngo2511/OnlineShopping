using ONLINE_SHOPPING.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ONLINE_SHOPPING.BO.Commons
{
    public class AuthenticateUpdateCookie : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            string controllerName = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
            string actionName = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();

            HttpCookie cookie = new HttpCookie(Commons.Constant.UserCookie);
            cookie.Values["LastController"] = controllerName;
            cookie.Values["LastAction"] = actionName;
            cookie.Expires = DateTime.Now.AddDays(10);

            filterContext.HttpContext.Response.SetCookie(cookie);

            base.OnActionExecuted(filterContext);
        }
    }
}