using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TiendaVirtual.Web
{
    using System.IO;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null,
                "",
                new { controller = "Productos", action = "Index", pagina = 1, categoria = (string)null }
            );

            routes.MapRoute(null,
                "pagina{pagina}",
                new { controller = "Productos", action = "Index", categoria = (string)null }
            );

            routes.MapRoute(null,
                "{categoria}",
                new { controller = "Productos", action = "Index", pagina = 1 }
            );

            routes.MapRoute(null,
                "{categoria}/pagina{pagina}",
                new { controller = "Productos", action = "Index" },
                new { pagina = @"\d+" }
            );


            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Productos", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute("Producto-Detalles"
                ,"Productos/{id}",
                new { controller = "Productos", action = "Detalle" });

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ConfigureLogging();

        }

        private static void ConfigureLogging()
        {
            log4net.Config.XmlConfigurator.Configure(
                new FileInfo(HttpContext.Current.Server.MapPath("~/log4net.config")));
        }
    }
}