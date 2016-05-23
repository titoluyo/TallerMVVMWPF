using System;
using System.ComponentModel.DataAnnotations;

namespace TiendaVirtual.Web.Areas.Administracion.Models
{
    public class LoginViewModel
    {
        [Required]
        public String Usuario { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }
    }
}