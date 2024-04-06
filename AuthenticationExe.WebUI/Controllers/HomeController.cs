using Microsoft.AspNetCore.Mvc;

namespace AuthenticationExe.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult TermsOfService()
        {
            return View();
        }
    }
}
