using Uniq.DAL.Entities;

namespace Uniq.WebUI.ViewModels
{
    public class ProductDetailVM
    {
        public Product Product { get; set; }
        public List<Product> ReleatedProducts { get; set; }

    }
}
