using System;
using System.Collections.Generic;
using System.Linq;

namespace TiendaVirtual.Web.Areas.Administracion.Models
{
    using System.Web.Mvc;

    using TiendaVirtual.Domain;
    public class EditarProductoViewModel
    {
        public EditarProductoViewModel(Producto producto
            , IEnumerable<Categoria> categorias)
        {
            this.Id = producto.Id;
            this.Nombre = producto.Nombre;
            this.Precio = producto.Precio.ToString();
            this.TieneImagen = producto.Imagen.Nombre != null;
            this.Categorias = categorias.Select(x =>
                new SelectListItem
                {
                    Text = x.Nombre,
                    Value = x.Id.ToString(),
                    Selected = producto.CategoriaId == x.Id
                });
        }
        
        public int Id { get; set; }

        public String Nombre { get; set; }

        public String Precio { get; set; }

        public DateTime? InicioVentas { get; set; }

        public bool TieneImagen { get; set; }

        public IEnumerable<SelectListItem> Categorias { get; set; }
    }
}