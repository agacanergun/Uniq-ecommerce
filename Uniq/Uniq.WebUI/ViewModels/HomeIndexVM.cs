using Uniq.DAL.Entities;

namespace Uniq.WebUI.ViewModels
{
    public class HomeIndexVM
    {
        public List<Product> UniqProducts { get; set; }
        public List<Product> BestSalesProducts { get; set; }
        public List<Product> MainPageProducts { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public List<Category> Categories { get; set; }
    }
}
