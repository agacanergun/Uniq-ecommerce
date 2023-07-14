using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Uniq.WebUI.Controllers
{
    [Authorize(AuthenticationSchemes = "UniqMemberAuth")]
    public class MemberProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
