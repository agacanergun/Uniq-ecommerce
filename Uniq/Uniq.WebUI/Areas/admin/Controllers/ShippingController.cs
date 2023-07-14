using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Uniq.BL.Repositories;
using Uniq.DAL.Entities;

namespace Uniq.WebUI.Areas.admin.Controllers
{

    [Area("admin"), Authorize(AuthenticationSchemes = "UniqAdminAuth")]
    public class ShippingController : Controller
    {
        IRepository<Shipping> repoShipping;
        public ShippingController(IRepository<Shipping> repoShipping)
        {
            this.repoShipping = repoShipping;
        }
        [Route("admin/kargolar")]
        public IActionResult Index()
        {
            var response = repoShipping.GetAll().OrderBy(x => x.DisplayIndex).ToList();
            return View(response);
        }
        [Route("admin/kargolar/ekle")]
        public IActionResult Add()
        {
            return View();
        }
        [Route("admin/kargolar/ekle"), HttpPost]
        public async Task<IActionResult> Add(Shipping Shipping)
        {
            if (ModelState.IsValid)
            {
                await repoShipping.Add(Shipping);
                return Redirect("/admin/kargolar");
            }
            ViewBag.Error = "Ekleme İşlemi Başarısız";
            return View(Shipping);
        }
        [Route("admin/kargolar/guncelle")]
        public IActionResult Update(int id)
        {
            return View(repoShipping.GetBy(x => x.ID == id));
        }
        [Route("admin/kargolar/guncelle"), HttpPost]
        public async Task<IActionResult> Update(Shipping Shipping)
        {
            if (ModelState.IsValid)
            {
                await repoShipping.Update(Shipping);
                return Redirect("/admin/kargolar");
            }
            ViewBag.Error = "Güncelleme İşlemi Başarısız";
            return View(Shipping);
        }

        [Route("admin/kargolar/sil")]
        public async Task<string> Delete(int id)
        {
            Shipping Shipping = new Shipping
            {
                ID = id,
            };
            await repoShipping.Delete(Shipping);
            return "Ok";
        }
    }

}
