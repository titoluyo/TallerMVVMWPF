namespace TiendaVirtual.Persistence
{
    using TiendaVirtual.Domain;
    using System.Collections.Generic;
    using System.Linq;
    using TiendaVirtual.Persistence.Extensions;
    using TiendaVirtual.Domain.Pagination;

    public class ProductosRepository
    {
        private readonly DataContext context = new DataContext();

        public Producto ById(int id)
        {
            return context.Productos.Single(x => x.Id == id);
        }

        public Producto PorNombre(string nombre)
        {
            return context.Productos.SingleOrDefault(x => x.Nombre.ToLower() == nombre.ToLower());
        }

        public IEnumerable<Producto> Todos()
        {
            return context.Productos.Include("Categoria").ToList();
        }

        public PagedList<Producto> Buscar(string categoria, int pagina, int tamañoPagina)
        {
            var productos = categoria == null
                ? context.Productos.OrderBy(x=>x.Nombre)
                : context.Productos.OrderBy(x => x.Nombre).Where(x => x.Categoria.Nombre == categoria);

            return productos.ToPagedList(pagina, tamañoPagina);
        }

        public IEnumerable<Producto> Buscar(string query)
        {
            return context.Productos.Where(x => x.Nombre.Contains(query)).ToList();
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Save(Producto producto)
        {
            context.Productos.AddObject(producto);
        }

        public void Delete(int id)
        {
            var producto = this.ById(id);
            context.Productos.DeleteObject(producto);
        }
    }
}
