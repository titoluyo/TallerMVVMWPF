using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Views.Models
{
    public class Post
    {
        public int Id { get; set; }

        public String Titulo { get; set; }

        public String Contenido { get; set; }

        public IList<Comentario> Comentarios { get; set; }
    }
}