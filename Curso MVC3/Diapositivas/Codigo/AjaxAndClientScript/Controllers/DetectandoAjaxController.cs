using System.Web.Mvc;

namespace AjaxAndClientScript.Controllers
{
    public class DetectandoAjaxController : Controller
    {
        //
        // GET: /DetectandoAjax/

        public ActionResult Delete()
        {
            if (Request.IsAjaxRequest())
            {
                return Content("OK");
            }
            return RedirectToAction("Index");
        }

    }
}
