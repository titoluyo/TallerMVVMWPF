using System.Web.Mvc;

namespace Models.Controllers
{
    using global::Models.Models;

    public class MostrandoErroresController : Controller
    {
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Producto product)
        {
            ModelState.AddModelError("","error general");
            if (!ModelState.IsValid) 
                return View();

            return Content("Correcto");
        }

    }
}
