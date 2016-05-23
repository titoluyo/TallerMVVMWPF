using System.Web.Mvc;
using Models.Models;

namespace Models.Controllers
{
    public class ValidationController : Controller
    {
        public ActionResult Crear(Producto producto)
        {
            if (string.IsNullOrEmpty(producto.Nombre))
                ModelState.AddModelError("Nombre", "Por favor, ingrese el nombre");

            if (producto.Precio == 0)
                ModelState.AddModelError("Precio", "El precio debe ser mayor a 0");

            if (!ModelState.IsValid)
            {
                return View();//si hay error retorna a la misma vista
            }

            return RedirectToAction("Index");
        }
    }
}
