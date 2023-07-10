using Microsoft.AspNetCore.Mvc;
using Uniq.BL.Repositories;
using Uniq.DAL.Entities;

namespace Uniq.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<CustomerServiceInstitutional> repocustomerServiceInstitutional;
        IRepository<Product> repoProduct;
        IRepository<ProductCategory> repoProductCategory;
        public HomeController(IRepository<CustomerServiceInstitutional> repocustomerServiceInstitutional, IRepository<Product> repoProduct, IRepository<ProductCategory> repoProductCategory)
        {
            this.repocustomerServiceInstitutional = repocustomerServiceInstitutional;
            this.repoProduct = repoProduct;
            this.repoProductCategory = repoProductCategory;
        }
        public IActionResult Index()
        {
            var Uniq = repoProduct.GetAll(x => x.SuggestedUnique == true).OrderBy(x => x.DisplayIndex).ToList();
            var BestSalesProducts = repoProduct.GetAll().OrderBy(o => Guid.NewGuid()).Take(4).ToList();
            var categories = repoProductCategory.GetAll().ToList();
            //çok satanlar getirilecek

            return View();
        }
        [Route("Detay/{title}-{id}")]
        public IActionResult CSIDetail(int id, string title)
        {
            return View(repocustomerServiceInstitutional.GetBy(x => x.ID == id));
        }
    }
}
