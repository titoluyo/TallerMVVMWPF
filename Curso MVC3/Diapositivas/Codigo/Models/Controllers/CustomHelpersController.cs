using System.Web.Mvc;

namespace Models.Controllers
{
    using global::Models.Models;

    public class CustomHelpersController : Controller
    {
        public ActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Test(CustomHelperModel testModel)
        {
            if (!ModelState.IsValid)
                return View();
            return RedirectToAction("Introduccion");
        }
    }
}
