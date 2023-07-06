using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Uniq.BL.Repositories;
using Uniq.DAL.Entities;

namespace Uniq.WebUI.Areas.admin.Controllers
{
    [Area("admin"), Authorize(AuthenticationSchemes = "UniqAdminAuth")]
    public class CategoryController : Controller
    {
        IRepository<Category> repoCategory;
        public CategoryController(IRepository<Category> repoCategory)
        {
            this.repoCategory = repoCategory;
        }
        [Route("admin/kategoriler")]
        public IActionResult Index()
        {
            var response = repoCategory.GetAll().OrderBy(x => x.DisplayIndex).ToList();
            return View(response);
        }
        [Route("admin/kategoriler/ekle")]
        public IActionResult Add()
        {
            return View();
        }
        [Route("admin/kategoriler/ekle"), HttpPost]
        public async Task<IActionResult> Add(Category category)
        {
            if (ModelState.IsValid)
            {
                await repoCategory.Add(category);
                return Redirect("/admin/kategoriler");
            }
            ViewBag.Error = "Ekleme İşlemi Başarısız";
            return View(category);
        }
        [Route("admin/kategoriler/guncelle")]
        public IActionResult Update(int id)
        {
            return View(repoCategory.GetBy(x => x.ID == id));
        }
        [Route("admin/kategoriler/guncelle"), HttpPost]
        public async Task<IActionResult> Update(Category category)
        {
            if (ModelState.IsValid)
            {
                await repoCategory.Update(category);
                return Redirect("/admin/kategoriler");
            }
            ViewBag.Error = "Güncelleme İşlemi Başarısız";
            return View(category);
        }

        public string Delete(int id)
        {
            return "Ok";
        }
    }
}
