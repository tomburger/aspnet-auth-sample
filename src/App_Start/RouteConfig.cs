using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AuthSample
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "Account", action = "Login" }
            );
            routes.MapRoute(
                name: "First",
                url: "first",
                defaults: new { controller = "Home", action = "Index" }
            );
            routes.MapRoute(
                name: "Second",
                url: "second",
                defaults: new { controller = "Home", action = "Second" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{id2}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional, id2 = UrlParameter.Optional }
            );
        }
    }
}
