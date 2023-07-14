using Microsoft.AspNetCore.Mvc;

namespace Uniq.WebUI.Controllers
{
    public class MemberProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
