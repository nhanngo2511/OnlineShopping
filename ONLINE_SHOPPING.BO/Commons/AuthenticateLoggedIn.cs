using ONLINE_SHOPPING.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ONLINE_SHOPPING.BO.Commons
{
    public class AuthenticateLoggedIn : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (User)System.Web.HttpContext.Current.Session[Commons.Constant.UserSession];

            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Account", action = "Login" }));         
            }
            base.OnActionExecuting(filterContext);
        }
    }
}