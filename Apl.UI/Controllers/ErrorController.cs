using System.Web.Mvc;

namespace Apl.UI.Controllers
{
    public class ErrorController : Controller
    {
        // The 404 action handler
        // Get: /FailWhale/
        public ActionResult FailWhale()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }

    }
}
