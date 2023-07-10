using Uniq.DAL.Entities;

namespace Uniq.WebUI.ViewModels
{
    public class HomeIndexVM
    {
        public List<Product> UniqProducts { get; set; }
        public List<Product> BestSalesProducts { get; set; }
        public List<ProductCategory> Categories { get; set; }
    }
}
