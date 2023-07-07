using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Uniq.BL.Repositories;
using Uniq.DAL.Entities;

namespace Uniq.WebUI.Areas.admin.Controllers
{
    [Area("admin"), Authorize(AuthenticationSchemes = "UniqAdminAuth")]
    public class CommunicationController : Controller
    {
        IRepository<Communication> repoCommunication;
        public CommunicationController(IRepository<Communication> repoCommunication)
        {
            this.repoCommunication = repoCommunication;
        }
        [Route("admin/iletisim")]
        public IActionResult Index()
        {
            var response = repoCommunication.GetAll().OrderBy(x => x.DisplayIndex).ToList();
            return View(response);
        }

        [Route("admin/iletisim/ekle")]
        public IActionResult Add()
        {
            return View();
        }
        [Route("admin/iletisim/ekle"), HttpPost]
        public async Task<IActionResult> Add(Communication model)
        {
            if (ModelState.IsValid)
            {
                await repoCommunication.Add(model);
                return Redirect("/admin/iletisim");
            }
            ViewBag.Error = "Ekleme İşlemi Başarısız";
            return View(model);
        }
        [Route("admin/iletisim/guncelle")]
        public IActionResult Update(int id)
        {
            return View(repoCommunication.GetBy(x => x.ID == id));
        }
        [Route("admin/iletisim/guncelle"), HttpPost]
        public async Task<IActionResult> Update(Communication model)
        {
            if (ModelState.IsValid)
            {
                await repoCommunication.Update(model);
                return Redirect("/admin/iletisim");
            }
            ViewBag.Error = "Güncelleme İşlemi Başarısız";
            return View(model);
        }

        [Route("admin/iletisim/sil")]
        public async Task<string> Delete(int id)
        {
            Communication entity = new Communication
            {
                ID = id,
            };
            await repoCommunication.Delete(entity);
            return "Ok";
        }
    }
}
