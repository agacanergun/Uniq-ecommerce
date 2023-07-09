using Uniq.DAL.Entities;

namespace Uniq.WebUI.Areas.admin.ViewModels
{
    public class ProductIndexVM
    {
        public List<Category> categories { get; set; }
        public Product Product { get; set; }
        public int[] CategoriyIDs { get; set; }
    }
}
