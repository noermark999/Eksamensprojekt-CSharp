using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Passenger",
                url: "Passenger/{action}/{id}",
                defaults: new { controller = "Passenger", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Car",
                url: "Car/{action}/{id}",
                defaults: new { controller = "Car", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Ferry", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
