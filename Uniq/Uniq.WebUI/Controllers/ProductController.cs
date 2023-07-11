using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uniq.BL.Repositories;
using Uniq.DAL.Entities;

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
            return View();
        }
    }
}
