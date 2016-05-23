using System.Web.Mvc;

// ReSharper disable Asp.NotResolved
namespace ControllersAndActions.Controllers
{
    public class FileResultController : Controller
    {
        public ActionResult DescargarReporte()
        {
            return File("c:\archivo.pdf","application/pdf","Reporte.pdf");
        }

    }
}
// ReSharper restore Asp.NotResolved
