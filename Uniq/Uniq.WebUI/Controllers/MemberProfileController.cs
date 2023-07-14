using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Uniq.BL.Repositories;
using Uniq.DAL.Entities;
using Uniq.WebUI.Tools;
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

        [Route("/profil")]
        public IActionResult Index()
        {
            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.PrimarySid)?.Value);
            var customer = repoCustomer.GetBy(x => x.Id == userId);
            if (customer != null)
            {
                if (customer.GuidId.ToString() == HttpContext.User.FindFirst(c => c.Type == "UserGuid")?.Value)
                {
                    MemberProfileVM memberProfileVM = new MemberProfileVM
                    {
                        Customer = customer,
                        CustomerAdressesList = repoCustomerAdresses.GetAll(x => x.CustomerID == userId).ToList(),
                    };
                    return View(memberProfileVM);
                }
            }
            return Redirect("/");
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

        [Route("/adres-sil")]
        public async Task<string> DeleteAdress(int id)
        {
            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.PrimarySid)?.Value);
            var customer = repoCustomer.GetBy(x => x.Id == userId);
            if (customer != null)
            {
                if (customer.GuidId.ToString() == HttpContext.User.FindFirst(c => c.Type == "UserGuid")?.Value)
                {
                    var adres = repoCustomerAdresses.GetBy(x => x.Id == id);
                    if (adres.CustomerID == userId)
                    {
                        await repoCustomerAdresses.Delete(adres);
                    }
                }
            }
            return "Ok";
        }

        public async Task<IActionResult> UpdateCustomer(MemberProfileVM model)
        {

                var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.PrimarySid)?.Value);
                if (model.Customer.Id == userId)
                {
                    if (model.Customer.GuidId.ToString() == HttpContext.User.FindFirst(c => c.Type == "UserGuid")?.Value)
                    {
                        var customer = repoCustomer.GetBy(x => x.Id == userId);
                        customer.Name = model.Customer.Name;
                        customer.Surname = model.Customer.Surname;
                        customer.PhoneNo = model.Customer.PhoneNo;
                        if (model.Customer.Password != null)
                        {
                            customer.Password = GeneralTool.getMD5(model.Customer.Password);
                        }
                        await repoCustomer.Update(customer);
                    }
                }
            TempData["Info"] = "Güncelleme Yapıldı.";
            return Redirect("/profil");
        }
    }
}
