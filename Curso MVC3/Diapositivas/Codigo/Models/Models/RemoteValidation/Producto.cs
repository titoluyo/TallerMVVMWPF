namespace Models.Models.RemoteValidation
{
    using System.Web.Mvc;

    public class Producto
    {
        [Remote("VerificarNombreUsuario", "Usuario")]
        public string Nombre { get; set; }
    }
}