using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using Uniq.BL.Repositories;
using Uniq.DAL.Entities;
using Uniq.WebUI.Models;
using Uniq.WebUI.ViewModels;

namespace Uniq.WebUI.Controllers
{
    [Authorize(AuthenticationSchemes = "UniqMemberAuth")]
    public class OrderController : Controller
    {
        IRepository<Shipping> repoShipping;
        IRepository<CustomerAdresses> repoCustomerAdresses;
        IRepository<Customer> repoCustomer;
        public OrderController(IRepository<Shipping> repoShipping, IRepository<CustomerAdresses> repoCustomerAdresses, IRepository<Customer> repoCustomer)
        {
            this.repoShipping = repoShipping;
            this.repoCustomerAdresses = repoCustomerAdresses;
            this.repoCustomer = repoCustomer;
        }
        public IActionResult Index()
        {
            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.PrimarySid)?.Value);
            var customer = repoCustomer.GetBy(x => x.Id == userId);
            if (customer != null)
            {
                if (customer.GuidId.ToString() == HttpContext.User.FindFirst(c => c.Type == "UserGuid")?.Value)
                {
                    var carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]);
                    decimal totalAmount = 0;
                    foreach (var item in carts)
                    {
                        totalAmount += item.Quantity * item.Price;
                    }
                    OrderVM orderVM = new OrderVM
                    {
                        TotalAmount = totalAmount,
                        CustomerAdresses = repoCustomerAdresses.GetAll(x => x.CustomerID == userId).ToList(),
                        Shippings = repoShipping.GetAll().OrderBy(x => x.DisplayIndex).ToList(),
                    };
                    return View(orderVM);
                }
                else
                    return Redirect("/");
            }
            else
                return Redirect("/");
        }
    }
}
