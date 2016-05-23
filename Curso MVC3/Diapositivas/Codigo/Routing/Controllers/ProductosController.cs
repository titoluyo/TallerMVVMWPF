using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Routing.Controllers
{
    using System.ComponentModel;

    public class ProductosController : Controller
    {
        //
        // GET: /Productos/

        public ActionResult Listar([DefaultValue("Libros")] String categoria)
        {
            return View();
        }

    }
}
