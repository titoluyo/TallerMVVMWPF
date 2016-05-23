namespace TiendaVirtual.Web.Areas.Administracion.Controllers
{
    using System.Configuration;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;
    using TiendaVirtual.Domain;
    using TiendaVirtual.Persistence;
    using TiendaVirtual.Web.Areas.Administracion.Models;

    [Authorize]
    public class HomeController : Controller
    {
        readonly CategoriasRepository categoriasRepository = new CategoriasRepository();

        readonly ProductosRepository productosRepository = new ProductosRepository();

        public ActionResult Index()
        {
            var productos = productosRepository.Todos();
            return View(productos);
        }

        public ActionResult Crear()
        {
            ViewBag.Categorias = categoriasRepository.Todos();
            return View(new Producto());
        }

        [HttpPost]
        public ActionResult Crear(Producto producto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categorias = categoriasRepository.Todos();
                return View();
            }
            productosRepository.Save(producto);
            productosRepository.Commit();
            TempData["Mensaje"] =
                "Se ha creado el producto: " + producto.Nombre;
            return RedirectToAction("index");
        }

        public ActionResult Editar(int id)
        {
            var producto = productosRepository.ById(id);
            var categorias = categoriasRepository.Todos();
            return View(new EditarProductoViewModel(producto, categorias));
        }

        [HttpPost]
        public ActionResult Editar(int id, HttpPostedFileBase archivo)
        {
            var producto = productosRepository.ById(id);
            UpdateModel(producto);
            if (archivo != null)
            {
                producto.Imagen = new Imagen{Nombre = archivo.FileName,
                                             Tipo = archivo.ContentType};
                var path=Path.Combine(ConfigurationManager.AppSettings["DirectorioImagenes"], archivo.FileName);
                archivo.SaveAs(path);
            }
            productosRepository.Commit();
            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            productosRepository.Delete(id);
            productosRepository.Commit();
            if (Request.IsAjaxRequest()) 
                return Content("OK");

            return RedirectToAction("Index");
        }

        public ActionResult ExisteNombre(string nombre)
        {
            var producto = productosRepository.PorNombre(nombre);
            var existe = producto == null;
            return Json(existe, JsonRequestBehavior.AllowGet);
        }

    }
}
