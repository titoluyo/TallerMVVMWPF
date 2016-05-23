using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// ReSharper disable Asp.NotResolved
namespace ControllersAndActions.Controllers
{
    using ControllersAndActions.Models;

    public class StronglyTypedController : Controller
    {
        public ViewResult DetalleProducto()
        {
            var producto = new Producto {Nombre = "MVC Cookbook",
                                         Precio = 15.5m};
            return View(producto);
        }


    }
}
// ReSharper restore Asp.NotResolved