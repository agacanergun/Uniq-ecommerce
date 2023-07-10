using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
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
        IRepository<ProductPicture> repoProductPicture;
        IWebHostEnvironment hostingEnvironment;
        public ProductController(IRepository<Product> repoProduct, IRepository<Category> repoCategory, IRepository<ProductCategory> repoProductCategory, IRepository<ProductPicture> repoProductPicture, IWebHostEnvironment hostingEnvironment)
        {
            this.repoProduct = repoProduct;
            this.repoCategory = repoCategory;
            this.repoProductCategory = repoProductCategory;
            this.repoProductPicture = repoProductPicture;
            this.hostingEnvironment = hostingEnvironment;
        }
        [Route("admin/urunler")]
        public IActionResult Index()
        {
            var response = repoProduct.GetAll().Include(i => i.ProductCategories).ThenInclude(t => t.Category).Include(x=>x.ProductPictures).ToList();
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


        [Route("/admin/urun/duzenle")]
        public IActionResult Update(int id)
        {
            ProductIndexVM productVM = new ProductIndexVM
            {
                categories = repoCategory.GetAll().OrderBy(x => x.Name).ToList(),
                Product = repoProduct.GetAll().Include(x => x.ProductCategories).FirstOrDefault(x => x.ID == id)
            };
            return View(productVM);
        }

        [Route("/admin/urun/duzenle"), HttpPost]
        public async Task<IActionResult> Update(ProductIndexVM model)
        {
            if (ModelState.IsValid)
            {
                decimal discountAmount = (model.Product.Price * Convert.ToInt64(model.Product.DiscountRate)) / 100;
                model.Product.DiscountedPrice = model.Product.Price - discountAmount;
                await repoProduct.Update(model.Product);
                await repoProductCategory.DeleteRange(repoProductCategory.GetAll(x => x.ProductID == model.Product.ID));
                if (model.CategoriyIDs.Length > 0)
                {
                    for (int i = 0; i < model.CategoriyIDs.Length; i++)
                    {
                        await repoProductCategory.Add(new ProductCategory { ProductID = model.Product.ID, CategoryID = model.CategoriyIDs[i] });
                    }
                }
                return Redirect("/admin/urunler");
            }
            return View(model);

        }

        [Route("/admin/urun/sil"), HttpPost]
        public async Task<string> Delete(int id)
        {
            Product product = repoProduct.GetBy(x => x.ID == id) ?? null;
            if (product != null)
            {
                var productPictures = repoProductPicture.GetAll(x => x.ProductID == id).ToList();
                foreach (var item in productPictures)
                {
                    string deleteFilePath = Path.Combine(hostingEnvironment.WebRootPath, item.Picture.TrimStart('/'));
                    if (System.IO.File.Exists(deleteFilePath))
                    {
                        System.IO.File.Delete(deleteFilePath);
                    }
                }
                await repoProduct.Delete(product);
            }
            return "Ok";
        }
    }
}
