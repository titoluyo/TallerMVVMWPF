using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TiendaVirtual.Persistence
{
    using TiendaVirtual.Domain;

    public class CategoriasRepository
    {
        public IEnumerable<Categoria> Todos()
        {
            using (DataContext context = new DataContext())
            {
                return context.Categorias.ToList();
            }
        }
    }
}
