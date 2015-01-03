using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ContactApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
             name: "Kontakti",
             url: "Routes/ContactList",
             defaults: new { controller = "Routes", action = "Contactlist" });

            routes.MapRoute(
            name: "Home actions",
            url: "Home/{action}/{id}",
           defaults: new { controller = "Home", action = "Read", id=0 });

            routes.MapRoute(
          name: "Pisi",
          url: "Home/Insert",
          defaults: new { controller = "Home", action = "Insert" });
                   
           routes.MapRoute(
           name: "NewContact",
           url: "Routes/{id}",
           defaults: new { controller = "Routes", action = "NewContact" });

        routes.MapRoute(
        name: "Default",
        url: "{*url}",
        defaults: new { controller = "Home", action = "Index" });

        }
    }
}
