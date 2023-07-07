using Microsoft.AspNetCore.Mvc;
using Uniq.BL.Repositories;
using Uniq.DAL.Entities;

namespace Uniq.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<CustomerServiceInstitutional> repocustomerServiceInstitutional;
        public HomeController(IRepository<CustomerServiceInstitutional> repocustomerServiceInstitutional)
        {
            this.repocustomerServiceInstitutional = repocustomerServiceInstitutional;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("Detay/{title}-{id}")]
        public IActionResult CSIDetail(int id, string title)
        {
            return View(repocustomerServiceInstitutional.GetBy(x => x.ID == id));
        }
    }
}
