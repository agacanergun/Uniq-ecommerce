using Uniq.DAL.Entities;

namespace Uniq.WebUI.ViewModels
{
    public class OrderVM
    {
        public decimal TotalAmount { get; set; }
        public List<Shipping> Shippings { get; set; }
        public List<CustomerAdresses> CustomerAdresses { get; set; }
    }
}
