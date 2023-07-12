using Microsoft.AspNetCore.Mvc;

namespace Uniq.WebUI.Controllers
{
    public class LoginAndRegisterController : Controller
    {
        [Route("/Giris-Yap")]
        public IActionResult Index()
        {
            return View();
        }

        //[Route("/Giris-Yap"),HttpPost]
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [Route("/Kayıt-Ol")]
        public IActionResult Register()
        {
            return View();
        }


        //[Route("/Kayıt-Ol"),HttpPost]
        //public IActionResult Register()
        //{
        //    return View();
        //}
    }
}
