using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Uniq.BL.Repositories;
using Uniq.DAL.Entities;
using Uniq.WebUI.Tools;

namespace Uniq.WebUI.Areas.admin.Controllers
{
    [Area("admin"), Authorize(AuthenticationSchemes = "UniqAdminAuth")]
    public class HomeController : Controller
    {
        IRepository<Admin> repoAdmin;
        public HomeController(IRepository<Admin> repoAdmin)
        {
            this.repoAdmin = repoAdmin;
        }

        [Route("/admin"), AllowAnonymous]
        public IActionResult Index(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [Route("/admin"), HttpPost, AllowAnonymous]
        public async Task<IActionResult> Index(Admin model, string ReturnUrl)
        {
            //admin paneline giriş için cookie oluşturma
            string md5Password = GeneralTool.getMD5(model.Password);
            Admin admin = repoAdmin.GetBy(x => x.UserName == model.UserName && x.Password == md5Password) ?? null;
            if (admin != null)
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.PrimarySid,admin.ID.ToString()),
                    new Claim(ClaimTypes.Name, admin.Name+" "+admin.Surname),
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "UniqAdminAuth");
                await HttpContext.SignInAsync("UniqAdminAuth", new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties() { IsPersistent = true });
                if (string.IsNullOrEmpty(ReturnUrl))
                    return Redirect("/admin/adminler");
                else return Redirect(ReturnUrl);
            }
            else
            {
                ViewBag.Error = "Geçersiz Kullanıcı Adı veya Şifre";
            }
            return View();
        }

        [Route("/admin/logout")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");

        }
    }
}
