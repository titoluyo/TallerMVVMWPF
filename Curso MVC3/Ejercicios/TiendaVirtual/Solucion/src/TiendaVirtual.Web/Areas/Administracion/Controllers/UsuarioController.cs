using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TiendaVirtual.Web.Areas.Administracion.Controllers
{
    using System.Web.Security;
    using TiendaVirtual.Web.Areas.Administracion.Models;

    public class UsuarioController : Controller
    {
        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
                if (!Autenticar(model.Usuario, model.Password))
                    ModelState.AddModelError("", "El usuario o password son incorrectos");

            if (!ModelState.IsValid)
                return View();

            FormsAuthentication.SetAuthCookie(model.Usuario, false);
            return Redirect(returnUrl ?? Url.Action("Index", "Home"));
        }

        private bool Autenticar(string usuario, string password)
        {
            return usuario == "admin" && password == "password";
        }
    }
}
