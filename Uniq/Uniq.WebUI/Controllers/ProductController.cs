using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uniq.BL.Repositories;
using Uniq.DAL.Entities;
using Uniq.WebUI.ViewModels;

namespace Uniq.WebUI.Controllers
{
    public class ProductController : Controller
    {
        IRepository<Product> repoProduct;
        public ProductController(IRepository<Product> _repoProduct)
        {
            repoProduct = _repoProduct;
        }

        [Route("/urundetay/{name}-{id}")]
        public IActionResult Index(string name, int id)
        {
            var product = repoProduct.GetAll().Include(x => x.ProductPictures).FirstOrDefault(x => x.ID == id);
            var releatedProducts = repoProduct.GetAll().Include(x => x.ProductPictures).OrderBy(o => Guid.NewGuid()).Take(5).ToList();
            ProductDetailVM vm = new ProductDetailVM
            {
                Product = product,
                ReleatedProducts = releatedProducts,
            };
            return View(vm);
        }
    }
}
