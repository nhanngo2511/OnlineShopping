using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ONLINE_SHOPPING.BO.Helpers
{
    public static class ViewUltilities
    {
        public static string RenderViewToString(ControllerContext context, string viewPath, Object Model = null, bool partial = false)
        {
            // first find the ViewEngine for this view
            ViewEngineResult viewEngineResult = null;
            if (partial)
                viewEngineResult = ViewEngines.Engines.FindPartialView(context, viewPath);
            else
                viewEngineResult = ViewEngines.Engines.FindView(context, viewPath, null);

            if (viewEngineResult == null)
                throw new FileNotFoundException("View cannot be found.");

            // get the view and attach the model to view data
            var view = viewEngineResult.View;
            context.Controller.ViewData.Model = Model;

            string result = null;

            using (var sw = new StringWriter())
            {
                var ctx = new ViewContext(context, view,
                                            context.Controller.ViewData,
                                            context.Controller.TempData,
                                            sw);
                view.Render(ctx, sw);
                result = sw.ToString();
            }

            return result;
        }


        public static string RenderViewToString02(this Controller Controller, string ViewPath, object model)
        {
            Controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var ViewResult = ViewEngines.Engines.FindPartialView(Controller.ControllerContext, ViewPath);
                var ViewContext = new ViewContext(Controller.ControllerContext, ViewResult.View, Controller.ViewData, Controller.TempData, sw);
                ViewResult.View.Render(ViewContext, sw);

                return sw.GetStringBuilder().ToString();

            }
        }     
    }
}