namespace TiendaVirtual.Domain
{
    using System.ComponentModel.DataAnnotations;
    using TiendaVirtual.Domain.Attributes;
    using System.Web.Mvc;

    [MetadataType(typeof(ProductoValidation))]
    public partial class Producto
    {
        public class ProductoValidation
        {
            [Required(ErrorMessage = "El nombre es obligatorio")]
            [Remote("ExisteNombre", "Home", ErrorMessage = "El nombre ya existe")]
            public string Nombre { get; set; }

            [Required(ErrorMessage = "El precio es obligatorio")]
            [Min(1,ErrorMessage = "El precio debe ser mayor a 0")]
            public decimal Precio { get; set; }
        }
    }
}