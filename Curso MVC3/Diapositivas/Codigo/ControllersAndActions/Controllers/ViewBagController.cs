using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

// ReSharper disable Asp.NotResolved
namespace ControllersAndActions.Controllers
{
    using ControllersAndActions.Models;

    public class ViewBagController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ViewResult DetalleProducto()
        {
            var producto = new Producto {Nombre = "MVC Cookbook", 
                                         Precio = 15.5m };
            ViewBag.Nombre = producto.Nombre;
            ViewBag.Precio = producto.Precio;
            return View();
        }

    }
}
// ReSharper restore Asp.NotResolved
