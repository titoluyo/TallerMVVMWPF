using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TiendaVirtual.Web.Controllers
{
    using System.ComponentModel;
    using System.Configuration;
    using System.IO;

    using TiendaVirtual.Persistence;
    using TiendaVirtual.Web.Filters;

    [Logging]
    public class ProductosController : Controller
    {
        const int TamañoPagina = 3;

        readonly ProductosRepository productosRepository = new ProductosRepository();

        [Logging]
        public ActionResult Index(String categoria, int pagina)
        {
            var productos = this.productosRepository.Buscar(categoria, pagina, TamañoPagina);
            return View(productos);
        }

        public ActionResult Buscar(String query)
        {
            var productos = this.productosRepository.Buscar(query);
            return View(productos);
        }

        public String Detalle(int id)
        {
            return "Detalle: " + id;
        }

        public ActionResult Imagen(int id)
        {
            var productos = this.productosRepository.ById(id);
            var path = Path.Combine(ConfigurationManager.AppSettings["DirectorioImagenes"], 
                       productos.Imagen.Nombre);
            return File(path, productos.Imagen.Tipo);
        }

    }
}
