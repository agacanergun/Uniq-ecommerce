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
        IRepository<ProductCategory> repoProductCategory;
        public ProductController(IRepository<Product> repoProduct, IRepository<Category> repoCategory, IRepository<ProductCategory> repoProductCategory)
        {
            this.repoProduct = repoProduct;
            this.repoCategory = repoCategory;
            this.repoProductCategory = repoProductCategory;

        }
        [Route("admin/urunler")]
        public IActionResult Index()
        {
            var response = repoProduct.GetAll().Include(i => i.ProductCategories).ThenInclude(t => t.Category).ToList();
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
            if (ModelState.IsValid)
            {
                decimal discountAmount = (model.Product.Price * Convert.ToInt64(model.Product.DiscountRate)) / 100;
                model.Product.DiscountedPrice = model.Product.Price - discountAmount;
                await repoProduct.Add(model.Product);
                if (model.CategoriyIDs.Length > 0)
                {
                    for (int i = 0; i < model.CategoriyIDs.Length; i++)
                    {
                        await repoProductCategory.Add(new ProductCategory { ProductID = model.Product.ID, CategoryID = model.CategoriyIDs[i] });
                    }
                }

                return Redirect("/admin/urunler");
            }
            ViewBag.Error = "Ekleme İşlemi Başarısız";
            return View(model);
        }
    }
}
