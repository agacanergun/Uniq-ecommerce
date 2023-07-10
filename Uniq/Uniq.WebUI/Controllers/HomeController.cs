using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uniq.BL.Repositories;
using Uniq.DAL.Entities;
using Uniq.WebUI.ViewModels;

namespace Uniq.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<CustomerServiceInstitutional> repocustomerServiceInstitutional;
        IRepository<Product> repoProduct;
        IRepository<ProductCategory> repoProductCategory;
        IRepository<Category> repoCategory;
        public HomeController(IRepository<CustomerServiceInstitutional> repocustomerServiceInstitutional, IRepository<Product> repoProduct, IRepository<ProductCategory> repoProductCategory, IRepository<Category> repoCategory)
        {
            this.repocustomerServiceInstitutional = repocustomerServiceInstitutional;
            this.repoProduct = repoProduct;
            this.repoProductCategory = repoProductCategory;
            this.repoCategory = repoCategory;
        }
        public IActionResult Index()
        {
            HomeIndexVM vm = new HomeIndexVM
            {
                BestSalesProducts = repoProduct.GetAll().Include(x => x.ProductPictures).OrderBy(o => Guid.NewGuid()).Take(5).ToList(),
                ProductCategories = repoProductCategory.GetAll().ToList(),
                UniqProducts = repoProduct.GetAll(x => x.SuggestedUnique == true).Include(x => x.ProductPictures).OrderBy(x => x.DisplayIndex).ToList(),
                Categories = repoCategory.GetAll().OrderBy(x => x.DisplayIndex).ToList(),
                MainPageProducts = repoProduct.GetAll(x => x.ShowOnMainPage == true).OrderBy(x => x.DisplayIndex).ToList(),
            };
            //çok satanlar getirilecek

            return View(vm);
        }
        [Route("Detay/{title}-{id}")]
        public IActionResult CSIDetail(int id, string title)
        {
            return View(repocustomerServiceInstitutional.GetBy(x => x.ID == id));
        }
    }
}
