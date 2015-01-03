using System.Web.Mvc;

namespace AwesomeAngularMVCApp.Controllers
{
    public class RoutesController : Controller
    {
        public ActionResult ContactList()
        {
            return View();
        }

        public ActionResult NewContact()
        {
            return View();
        }
    }
}