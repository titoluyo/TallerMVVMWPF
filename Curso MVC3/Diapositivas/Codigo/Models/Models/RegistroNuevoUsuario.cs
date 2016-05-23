using System.ComponentModel.DataAnnotations;
namespace Models.Models
{
    using System.Collections.Generic;

    public class RegistroNuevoUsuario : IValidatableObject
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmacionPassword { get; set; }

        public IEnumerable<ValidationResult>
            Validate(ValidationContext validationContext)
        {
            if (!Password.Equals(ConfirmacionPassword)) 
                yield return new ValidationResult(
                    "Los passwords deben ser iguales", 
                    new[] { "ConfirmacionPassword" });
        }
    }
}