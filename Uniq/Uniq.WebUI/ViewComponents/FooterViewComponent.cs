using Microsoft.AspNetCore.Mvc;
using Uniq.BL.Repositories;
using Uniq.DAL.Entities;
using Uniq.WebUI.ViewModels;

namespace Uniq.WebUI.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        IRepository<Communication> repoCommunication;
        IRepository<CustomerServiceInstitutional> repoCustomerServiceInstitutional;
        IRepository<SocialMedia> repoSocialMedia;
        public FooterViewComponent(IRepository<Communication> repoCommunication, IRepository<CustomerServiceInstitutional> repoCustomerServiceInstitutional, IRepository<SocialMedia> repoSocialMedia)
        {
            this.repoCommunication = repoCommunication;
            this.repoCustomerServiceInstitutional = repoCustomerServiceInstitutional;
            this.repoSocialMedia = repoSocialMedia;
        }
        public IViewComponentResult Invoke()
        {
            var communications = repoCommunication.GetAll().OrderBy(x => x.DisplayIndex).ToList();
            var CSIs = repoCustomerServiceInstitutional.GetAll().OrderBy(x => x.DisplayIndex).ToList();
            var socialMedia = repoSocialMedia.GetAll().FirstOrDefault();

            FooterVM vm = new FooterVM
            {
                Communications = communications,
                CustomerServiceInstitutionals = CSIs,
                SocialMedia = socialMedia,
            };
            return View(vm);
        }
    }
}
