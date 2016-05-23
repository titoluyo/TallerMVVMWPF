using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrimerProyecto.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class Comentario
    {
        public int Id { get; set; }

        [Required(ErrorMessage="este campo es obligatorio")]
        public String Titulo { get; set; }

        public String Mensaje { get; set; }
    }
}