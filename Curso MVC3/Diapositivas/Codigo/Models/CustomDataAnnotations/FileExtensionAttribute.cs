using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Models.CustomDataAnnotations
{
    public class FileExtensionsAttribute : ValidationAttribute
    {
        private readonly string extensions;
        public FileExtensionsAttribute(string extensions){
            this.extensions = extensions;
            ErrorMessage = "{0} no es una extensión válida";
        }

        public override bool IsValid(object value){
            return extensions.Split('|').Contains(value);
        }
    }
}