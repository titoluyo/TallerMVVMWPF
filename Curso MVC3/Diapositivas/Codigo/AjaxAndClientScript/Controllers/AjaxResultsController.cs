using System.Web.Mvc;

namespace AjaxAndClientScript.Controllers
{
    using AjaxAndClientScript.Models;

    public class AjaxResultsController : Controller
    {
        public ActionResult Content()
        {
            return Content("<strong>Success!</strong>");
        }

        public ActionResult Json()
        {
            if (Request.IsAjaxRequest())
            {
                var productos = new[]
                    {
                        new Producto { Nombre = "nombre1", Precio = "10" },
                        new Producto { Nombre = "nombre2", Precio = "20" }
                    };
                return Json(productos, JsonRequestBehavior.AllowGet);
            }
            return View();
        }

        public ActionResult PartialView()
        {
            if (Request.IsAjaxRequest())
            {
                var productos = new[]
                    {
                        new Producto { Nombre = "nombre1", Precio = "10" },
                        new Producto { Nombre = "nombre2", Precio = "20" }
                    };

                return PartialView("_Productos", productos);
            }
            return View();
        }

    }
}
