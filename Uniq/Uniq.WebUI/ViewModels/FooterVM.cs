using Uniq.DAL.Entities;

namespace Uniq.WebUI.ViewModels
{
    public class FooterVM
    {
        public List<Communication> Communications { get; set; }
        public List<CustomerServiceInstitutional> CustomerServiceInstitutionals { get; set; }
        public SocialMedia SocialMedia { get; set; }
    }
}
