using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// ReSharper disable Asp.NotResolved
namespace ControllersAndActions.Controllers
{
    using ControllersAndActions.Models;

    public class RedirectController : Controller
    {
        public ActionResult Listar()
        {
            return View();
        }
        //
        // GET: /Redirect/

        //public ActionResult Crear(Producto producto)
        //{
        //    GuardarProducto(producto);
        //    return View("Listar");
        //}

        public RedirectToRouteResult Crear(Producto producto)
        {
            GuardarProducto(producto);
            TempData["Mensaje"] = 
                        "El nuevo producto ha sido guardado";
            return RedirectToAction("Listar");
        }

        private void GuardarProducto(Producto producto)
        {
            throw new NotImplementedException();
        }
    }
}
// ReSharper restore Asp.NotResolved