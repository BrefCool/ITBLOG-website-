using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;

namespace BLOG_TEST
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapHttpRoute(
                name: "WebApiRoute",
                routeTemplate: "api/{Controller}/{id}",
                defaults: new {  id = UrlParameter.Optional }
            );
        }
    }
}
