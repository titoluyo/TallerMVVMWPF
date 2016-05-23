using System.Web.Mvc;

namespace Models.Controllers
{
    public class UsuarioController : Controller
    {
        public ActionResult VerificarNombreUsuario(string usuario)
        {
            var esValido = !usuario.Equals("Admin");
            return Json(esValido, JsonRequestBehavior.AllowGet);
        }
    }
}
