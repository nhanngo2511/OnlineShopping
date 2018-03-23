using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ONLINE_SHOPPING.APIs
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Enable Cros
            //config.EnableCors();
            //var cors = new EnableCorsAttribute(origins: "*", headers: "*", methods: "*");
            //config.EnableCors(cors);
           

            // Web API routes
            //config.MapHttpAttributeRoutes();

       //     config.MessageHandlers.Add(new AuthenticationHandler());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
