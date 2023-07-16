using Uniq.DAL.Entities;

namespace Uniq.WebUI.ViewModels
{
    public class MemberProfileVM
    {
        public CustomerAdresses CustomerAdresses { get; set; }
        public List<CustomerAdresses> CustomerAdressesList { get; set; }
        public Customer Customer { get; set; }
        public List<Order> Orders { get; set; }
    
    }
}
