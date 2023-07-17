using Microsoft.AspNetCore.Mvc;
using Uniq.BL.Repositories;
using Uniq.DAL.Entities;
using Uniq.WebUI.ViewModels;

namespace Uniq.WebUI.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        IRepository<Category> repoCategory;
        public HeaderViewComponent(IRepository<Category> repoCategory)
        {
            this.repoCategory = repoCategory;
        }
        public IViewComponentResult Invoke()
        {
            var categories = repoCategory.GetAll().ToList();
            return View(categories);
        }
    }
}
