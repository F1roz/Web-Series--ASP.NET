using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebSeries
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
<<<<<<< HEAD
                defaults: new { controller = "Video", action = "List", id = UrlParameter.Optional }
=======
                defaults: new { controller = "User", action = "List", id = UrlParameter.Optional }
>>>>>>> 31985e60f63dc55cfa441d0dd6a31f73365763ae
            );
        }

    }
}
