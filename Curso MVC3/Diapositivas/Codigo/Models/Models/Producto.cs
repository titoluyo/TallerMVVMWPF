using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Models.Models
{
    public class Producto
    {
        //[DisplayName("Nombre del Producto")]
        [Required(ErrorMessage = "Por favor, ingrese el nombre")]
        public string Nombre { get; set; }

        //[DisplayFormat(DataFormatString = "{0:c}")]
        [Range(1, 1000000, ErrorMessage = "El precio debe ser mayor a 0")]
        [Required(ErrorMessage = "Por favor, ingrese el precio")]
        public decimal Precio { get; set; }
    }


}