using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uniq.BL.Repositories;
using Uniq.DAL.Entities;
using Uniq.WebUI.Areas.admin.ViewModels;

namespace Uniq.WebUI.Areas.admin.Controllers
{
    [Area("admin"), Authorize(AuthenticationSchemes = "UniqAdminAuth")]
    public class ProductController : Controller
    {
        IRepository<Product> repoProduct;
        IRepository<Category> repoCategory;
        public ProductController(IRepository<Product> repoProduct, IRepository<Category> repoCategory)
        {
            this.repoProduct = repoProduct;
            this.repoCategory = repoCategory;

        }
        [Route("admin/urunler")]
        public IActionResult Index()
        {

            var response = repoProduct.GetAll().OrderBy(x => x.DisplayIndex).Include(x => x.ProductPictures).Include(x => x.ProductCategories).ToList();
            return View(response);
        }

        [Route("admin/urunler/ekle")]
        public IActionResult Add()
        {
            var categories = repoCategory.GetAll().ToList();
            ProductIndexVM vm = new ProductIndexVM
            {
                categories = categories,
            };
            return View(vm);
        }
        [Route("admin/urunler/ekle"), HttpPost]
        public async Task<IActionResult> Add(ProductIndexVM model)
        {
            //if (ModelState.IsValid)
            //{
            //    await repoProduct.Add(model);
            //    return Redirect("/admin/kategoriler");
            //}
            //ViewBag.Error = "Ekleme İşlemi Başarısız";
            //return View(model);
            return View();
        }
    }
}
