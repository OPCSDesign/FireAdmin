using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FireAdmin
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute(url: "{resource}.axd/{*pathInfo}");

			routes.MapRoute(
					name: "Brigade",
					url: "brigade/{action}/{id}",
					defaults: new { controller = "Brigade", action = "Index", id = UrlParameter.Optional }
			);

			routes.MapRoute(
					name: "Station",
					url: "station/{action}/{id}",
					defaults: new { controller = "Station", action = "Index", id = UrlParameter.Optional }
			);

            routes.MapRoute(
                    name: "StationType",
                    url: "stationtype/{action}/{id}",
                    defaults: new { controller = "Stationtype", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
					name: "Account",
					url: "account/{action}/{id}",
					defaults: new { controller = "Account", action = "Index", id = UrlParameter.Optional }
			);

			routes.MapRoute(
					name: "Roles",
					url: "roles/{action}/{id}",
					defaults: new { controller = "Roles", action = "Index", id = UrlParameter.Optional }
			);

			routes.MapRoute(
					name: "Default",
					url: "{action}/{id}",
					defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
