using System;

namespace Models.Models
{
    using global::Models.CustomDataAnnotations;

    public class CustomHelperModel
    {
        [FileExtensions("jpg|gif")]
        public String Archivo { get; set; }

        [GreaterThanNumber("Numero2")]
        public int Numero1 { get; set; }

        public int Numero2 { get; set; }
    }
}