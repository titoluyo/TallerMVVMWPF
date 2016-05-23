using System;

namespace Views.Models
{
    public class LoginViewModel
    {
        public String Usuario { get; set; }

        public String Contraseña { get; set; }

        public bool RecordarContraseña { get; set; }
    }
}