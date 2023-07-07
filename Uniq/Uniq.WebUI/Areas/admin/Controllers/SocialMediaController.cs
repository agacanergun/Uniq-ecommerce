using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Uniq.BL.Repositories;
using Uniq.DAL.Entities;

namespace Uniq.WebUI.Areas.admin.Controllers
{
    [Area("admin"), Authorize(AuthenticationSchemes = "UniqAdminAuth")]
    public class SocialMediaController : Controller
    {
        IRepository<SocialMedia> repoSocialMedia;
        public SocialMediaController(IRepository<SocialMedia> repoSocialMedia)
        {
            this.repoSocialMedia = repoSocialMedia;
        }
        [Route("admin/sosyalmedya")]
        public IActionResult Index()
        {
            var response = repoSocialMedia.GetAll().ToList();
            return View(response);
        }
        [Route("admin/sosyalmedya/ekle")]
        public IActionResult Add()
        {
            return View();
        }
        [Route("admin/sosyalmedya/ekle"), HttpPost]
        public async Task<IActionResult> Add(SocialMedia SocialMedia)
        {
            var response = repoSocialMedia.GetAll().FirstOrDefault();
            if (response != null)
            {
                ViewBag.Error = "Bu Kategoride Yanlızca Bir Adet Veri Ekleyebilirsiniz";
                return View(SocialMedia);
            }
            if (ModelState.IsValid)
            {
                await repoSocialMedia.Add(SocialMedia);
                return Redirect("/admin/sosyalmedya");
            }
            ViewBag.Error = "Ekleme İşlemi Başarısız";
            return View(SocialMedia);
        }
        [Route("admin/sosyalmedya/guncelle")]
        public IActionResult Update(int id)
        {
            return View(repoSocialMedia.GetBy(x => x.ID == id));
        }
        [Route("admin/sosyalmedya/guncelle"), HttpPost]
        public async Task<IActionResult> Update(SocialMedia SocialMedia)
        {
            if (ModelState.IsValid)
            {
                await repoSocialMedia.Update(SocialMedia);
                return Redirect("/admin/sosyalmedya");
            }
            ViewBag.Error = "Güncelleme İşlemi Başarısız";
            return View(SocialMedia);
        }

        [Route("admin/sosyalmedya/sil")]
        public async Task<string> Delete(int id)
        {
            SocialMedia SocialMedia = new SocialMedia
            {
                ID = id,
            };
            await repoSocialMedia.Delete(SocialMedia);
            return "Ok";
        }
    }
}
