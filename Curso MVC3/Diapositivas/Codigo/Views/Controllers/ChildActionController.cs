using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Views.Controllers
{
    public class ChildActionController : Controller
    {
        //[ChildActionOnly]
        //public ActionResult Menu()
        //{
        //    return View();
        //}

        public PartialViewResult Menu(){
            return PartialView();
}

    }
}
