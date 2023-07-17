using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Uniq.BL.Repositories;
using Uniq.DAL.Entities;

namespace Uniq.WebUI.Areas.admin.Controllers
{
    [Area("admin"), Authorize(AuthenticationSchemes = "UniqAdminAuth")]
    public class SmallSliderController : Controller
    {
        IRepository<SmallSlider> repoSmallSlider;
        public SmallSliderController(IRepository<SmallSlider> repoSmallSlider)
        {
            this.repoSmallSlider = repoSmallSlider;
        }
        [Route("admin/ufak-slayt")]
        public IActionResult Index()
        {
            var response = repoSmallSlider.GetAll().ToList();
            return View(response);
        }
        [Route("admin/ufak-slayt/ekle")]
        public IActionResult Add()
        {
            return View();
        }
        [Route("admin/ufak-slayt/ekle"), HttpPost]
        public async Task<IActionResult> Add(SmallSlider SmallSlider)
        {
            var response = repoSmallSlider.GetAll().ToList();
            if (response !=null)
            {
                ViewBag.Error = "Bu Alanda Yanlızca 1 Adet Veri Ekleyebilirsiniz.";
                return View(SmallSlider);
            }

            if (ModelState.IsValid)
            {
                await repoSmallSlider.Add(SmallSlider);
                return Redirect("/admin/ufak-slayt");
            }
            ViewBag.Error = "Ekleme İşlemi Başarısız";
            return View(SmallSlider);
        }
        [Route("admin/ufak-slayt/guncelle")]
        public IActionResult Update(int id)
        {
            return View(repoSmallSlider.GetBy(x => x.Id == id));
        }
        [Route("admin/ufak-slayt/guncelle"), HttpPost]
        public async Task<IActionResult> Update(SmallSlider SmallSlider)
        {
            if (ModelState.IsValid)
            {
                await repoSmallSlider.Update(SmallSlider);
                return Redirect("/admin/ufak-slayt");
            }
            ViewBag.Error = "Güncelleme İşlemi Başarısız";
            return View(SmallSlider);
        }

        [Route("admin/ufak-slayt/sil")]
        public async Task<string> Delete(int id)
        {
            SmallSlider SmallSlider = new SmallSlider
            {
                Id = id,
            };
            await repoSmallSlider.Delete(SmallSlider);
            return "Ok";
        }
    }
}
