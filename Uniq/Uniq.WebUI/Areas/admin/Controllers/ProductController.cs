using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Uniq.WebUI.Areas.admin.Controllers
{
    [Area("admin"), Authorize(AuthenticationSchemes = "UniqAdminAuth")]
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
