using System.ComponentModel.DataAnnotations;
namespace Models.Models.ClientValidation
{
    public class Producto
    {
        [Required]
        public string Nombre { get; set; }
    }
}