using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Views.Controllers
{
    public class LayoutViewsController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Views/Shared/LayoutViews/ContentView.cshtml");
        }
    }
}
