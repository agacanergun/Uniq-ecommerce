using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Uniq.BL.Repositories;
using Uniq.DAL.Entities;
using Uniq.WebUI.ViewModels;

namespace Uniq.WebUI.Controllers
{
    [Authorize(AuthenticationSchemes = "UniqMemberAuth")]
    public class MemberProfileController : Controller
    {
        IRepository<Customer> repoCustomer;
        IRepository<CustomerAdresses> repoCustomerAdresses;
        public MemberProfileController(IRepository<Customer> repoCustomer, IRepository<CustomerAdresses> repoCustomerAdresses)
        {
            this.repoCustomer = repoCustomer;
            this.repoCustomerAdresses = repoCustomerAdresses;

        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> AddAdress(MemberProfileVM model)
        {
            if (ModelState.IsValid)
            {
                var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.PrimarySid)?.Value);
                var customer = repoCustomer.GetBy(x => x.Id == userId);
                if (customer != null)
                {
                    if (customer.GuidId.ToString() == HttpContext.User.FindFirst(c => c.Type == "UserGuid")?.Value)
                    {
                        model.CustomerAdresses.CustomerID = userId;
                        await repoCustomerAdresses.Add(model.CustomerAdresses);
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}
