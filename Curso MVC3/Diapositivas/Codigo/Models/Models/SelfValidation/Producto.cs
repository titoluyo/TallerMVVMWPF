namespace Models.Models.SelfValidation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Producto : IValidatableObject
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Categoria { get; set; }
        [Required]
        public decimal Precio { get; set; }

        public IEnumerable<ValidationResult>
            Validate(ValidationContext validationContext)
        {
            if (Categoria.Equals("Autos") && Precio < 500)
                yield return new ValidationResult(
                "El precio debe ser mayor a 500 para los autos",
                new[] { "Precio" });
        }
    }
}