using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Views.Controllers
{
    using Views.Models;

    public class HomeController : Controller
    {
        public ActionResult InlineCode()
        {
            Post post = new Post
            {
                Titulo = "Hola MVC",
                Contenido = "Este es un post sobre mvc",
                Comentarios = new []{ new Comentario { Texto = "Buen Post" } }
            };
            return View(post);
        }

        public ActionResult HtmlHelpers()
        {
            ViewBag.Titulo = "Este es un titulo";
            return View();
        }

    }
}
