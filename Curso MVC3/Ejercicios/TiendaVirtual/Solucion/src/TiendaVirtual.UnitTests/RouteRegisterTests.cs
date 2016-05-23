using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;

namespace TiendaVirtual.UnitTests
{
    using TiendaVirtual.Web;
    using System.Web.Routing;
    using TiendaVirtual.Web.Controllers;

    [TestClass]
    public class RouteRegisterTests
    {
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            MvcApplication.RegisterRoutes(RouteTable.Routes);
        }


        [TestMethod]
        public void LasUrlsConcuerdanConLasRutasRegistradas()
        {
            "~/".ShouldMapTo<ProductosController>(action => action.Index(null, 1));
            "~/libros".ShouldMapTo<ProductosController>(action => action.Index("libros", 1));
            "~/pagina2".ShouldMapTo<ProductosController>(action => action.Index(null, 2));
            "~/libros/pagina2".ShouldMapTo<ProductosController>(action => action.Index("libros", 2));
        }
    }
}
