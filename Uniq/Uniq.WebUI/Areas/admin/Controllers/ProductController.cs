using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uniq.BL.Repositories;
using Uniq.DAL.Entities;

namespace Uniq.WebUI.Areas.admin.Controllers
{
    [Area("admin"), Authorize(AuthenticationSchemes = "UniqAdminAuth")]
    public class ProductController : Controller
    {
        IRepository<Product> repoProduct;
        public ProductController(IRepository<Product> repoProduct)
        {
            this.repoProduct = repoProduct;
        }
        [Route("admin/urunler")]
        public IActionResult Index()
        {
            
            var response = repoProduct.GetAll().OrderBy(x => x.DisplayIndex).Include(x => x.ProductPictures).Include(x=>x.ProductCategories).ToList();
            return View(response);
        }
    }
}
