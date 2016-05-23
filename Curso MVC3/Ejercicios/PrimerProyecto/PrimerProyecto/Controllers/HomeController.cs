using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrimerProyecto.Controllers
{
    using PrimerProyecto.Models;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Fecha = DateTime.Now;
            return View();
        }

        public ActionResult Comentarios()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Comentarios(Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                //Enviarlo por mail
                return View("Gracias", comentario);
            }
            return View();
        }

        public String Hola(string id)
        {
            return id;
        }
    }
}
