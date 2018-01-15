using Simplex.Tools.AppSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AmzWholeSaleWeb
{
    public class RouteConfig
    {

        public static void RegisterRoutes(RouteCollection routes)
        {
            string homeController = AppSettingsHandler.GetAppSettingsValue("HomeController");
            string homeView = "Index";

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = homeController, action = homeView, id = UrlParameter.Optional }
            );
        }
    }
}
