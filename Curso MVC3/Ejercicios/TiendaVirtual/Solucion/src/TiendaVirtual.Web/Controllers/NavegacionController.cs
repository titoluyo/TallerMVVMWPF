using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaVirtual.Persistence;

namespace TiendaVirtual.Web.Controllers
{
    public class NavegacionController : Controller
    {
        readonly CategoriasRepository categoriasRepository=new CategoriasRepository();
        [ChildActionOnly]
        public ActionResult Menu()
        {
            var categorias = categoriasRepository.Todos();
            return View("_Menu",categorias);
        }

    }
}
