using Microsoft.AspNetCore.Mvc;

namespace Uniq.WebUI.Controllers
{
    public class CartController : Controller
    {
        [Route("/sepetim")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
