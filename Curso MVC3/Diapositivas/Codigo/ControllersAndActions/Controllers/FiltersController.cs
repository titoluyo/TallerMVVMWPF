using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

// ReSharper disable Asp.NotResolved
namespace ControllersAndActions.Controllers
{
    public class FiltersController : Controller
    {
        [Authorize]
        public ActionResult AdministrarPedidos()
        {
            return View();
        }

    }

}
// ReSharper restore Asp.NotResolved
