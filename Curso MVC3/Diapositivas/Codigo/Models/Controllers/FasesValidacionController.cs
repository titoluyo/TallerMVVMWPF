using System.Web.Mvc;

namespace Models.Controllers
{
    using global::Models.Models;

    public class FasesValidacionController : Controller
    {
        public ActionResult Editar(Producto producto)
        {
            if (!ModelState.IsValid)
                return View();
            //Actualizar el producto en la BD
            return RedirectToAction("Index");
        }
    }
}
