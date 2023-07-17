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
        IRepository<Slider> repoSlider;
        IRepository<SmallSlider> repoSmallSlider;
        IRepository<SoldProduct> repoSoldProduct;
        public HomeController(IRepository<CustomerServiceInstitutional> repocustomerServiceInstitutional, IRepository<Product> repoProduct, IRepository<ProductCategory> repoProductCategory, IRepository<Category> repoCategory, IRepository<Slider> repoSlider, IRepository<SmallSlider> repoSmallSlider, IRepository<SoldProduct> repoSoldProduct)
        {
            this.repocustomerServiceInstitutional = repocustomerServiceInstitutional;
            this.repoProduct = repoProduct;
            this.repoProductCategory = repoProductCategory;
            this.repoCategory = repoCategory;
            this.repoSlider = repoSlider;
            this.repoSmallSlider = repoSmallSlider;
            this.repoSoldProduct = repoSoldProduct;
        }
        public IActionResult Index()
        {

            var top5Products = repoSoldProduct.GetAll()
                   .GroupBy(p => p.ProductId)
                   .Select(g => new
                   {
                       ProductId = g.Key,
                       TotalSoldQuantity = g.Sum(p => p.Quantity)
                   })
                   .OrderByDescending(g => g.TotalSoldQuantity)
                   .Take(5).ToList();


            List<Product> bestSalesProducts = new List<Product>();

            foreach (var item in top5Products)
            {
                var product = repoProduct.GetBy(x => x.ID == item.ProductId);
                bestSalesProducts.Add(product);
            }

            HomeIndexVM vm = new HomeIndexVM
            {
                BestSalesProducts = bestSalesProducts,
                ProductCategories = repoProductCategory.GetAll().ToList(),
                UniqProducts = repoProduct.GetAll(x => x.SuggestedUnique == true).Include(x => x.ProductPictures).OrderBy(x => x.DisplayIndex).ToList(),
                Categories = repoCategory.GetAll().OrderBy(x => x.DisplayIndex).ToList(),
                MainPageProducts = repoProduct.GetAll(x => x.ShowOnMainPage == true).OrderBy(x => x.DisplayIndex).ToList(),
                Slider = repoSlider.GetAll().FirstOrDefault(),
                SmallSlider = repoSmallSlider.GetAll().FirstOrDefault(),
            };

            return View(vm);
        }
        [Route("Detay/{title}-{id}")]
        public IActionResult CSIDetail(int id, string title)
        {
            return View(repocustomerServiceInstitutional.GetBy(x => x.ID == id));
        }

        [Route("Kategoriler/{categoryname}-{categoryid}")]
        public IActionResult Category(string categoryname, int categoryid)
        {
            var productCategories = repoProductCategory.GetAll(x => x.CategoryID == categoryid);

            var productIds = productCategories.Select(pc => pc.ProductID).ToList();

            var products = repoProduct
                .GetAll(x => productIds.Contains(x.ID))
                .Include(x => x.ProductPictures)
                .OrderBy(x => x.DisplayIndex)
                .ToList();


            var top5Products = repoSoldProduct.GetAll()
                .GroupBy(p => p.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalSoldQuantity = g.Sum(p => p.Quantity)
                })
                .OrderByDescending(g => g.TotalSoldQuantity)
                .Take(5).ToList();


            List<Product> bestSalesProducts = new List<Product>();

            foreach (var item in top5Products)
            {
                var product = repoProduct.GetBy(x => x.ID == item.ProductId);
                bestSalesProducts.Add(product);
            }


            HomeIndexVM vm = new HomeIndexVM
            {
                BestSalesProducts = bestSalesProducts,
                ProductCategories = repoProductCategory.GetAll().ToList(),
                UniqProducts = repoProduct.GetAll(x => x.SuggestedUnique == true).Include(x => x.ProductPictures).OrderBy(x => x.DisplayIndex).ToList(),
                Categories = repoCategory.GetAll().OrderBy(x => x.DisplayIndex).ToList(),
                MainPageProducts = products,
                Slider = repoSlider.GetAll().FirstOrDefault(),
                SmallSlider = repoSmallSlider.GetAll().FirstOrDefault(),
            };
            ViewBag.categoryname = categoryname;
            return View(vm);
        }

    }
}
