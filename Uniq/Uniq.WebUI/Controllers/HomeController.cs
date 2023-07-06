using Microsoft.AspNetCore.Mvc;

namespace Uniq.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
