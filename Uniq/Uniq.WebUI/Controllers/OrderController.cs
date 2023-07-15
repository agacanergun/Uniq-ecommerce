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
        IRepository<Order> repoOrder;
        IRepository<SoldProduct> repoSoldProduct;
        IRepository<Product> repoProduct;
        public OrderController(IRepository<Shipping> repoShipping, IRepository<CustomerAdresses> repoCustomerAdresses, IRepository<Customer> repoCustomer, IRepository<Order> repoOrder, IRepository<SoldProduct> repoSoldProduct, IRepository<Product> repoProduct)
        {
            this.repoShipping = repoShipping;
            this.repoCustomerAdresses = repoCustomerAdresses;
            this.repoCustomer = repoCustomer;
            this.repoOrder = repoOrder;
            this.repoSoldProduct = repoSoldProduct;
            this.repoProduct = repoProduct;
        }
        [Route("siparis-olustur")]
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

        public async Task<IActionResult> Complete(OrderVM vm)
        {
            if (vm.Order==null)
            {
                return Redirect("/siparis-olustur");
            }
            else
            {
                if (vm.Order.CustomerAddressId == 0 || vm.Order.ShippingType == null)
                {
                    return Redirect("/siparis-olustur");
                }
            }
      

            var userId = int.Parse(HttpContext.User.FindFirst(ClaimTypes.PrimarySid)?.Value);
            var customer = repoCustomer.GetBy(x => x.Id == userId);

            if (customer != null)
            {
                if (customer.GuidId.ToString() == HttpContext.User.FindFirst(c => c.Type == "UserGuid")?.Value)
                {
                    vm.Order.CustomerId = userId;
                    vm.Order.OrderNumber = Guid.NewGuid().ToString();
                    vm.Order.OrderStatus = "Sipariş Alındı";
                    vm.Order.Status = 1;
                    await repoOrder.Add(vm.Order);

                    List<Cart> carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]);
                    List<SoldProduct> soldProducts = new List<SoldProduct>();

                    foreach (var item in carts)
                    {
                        var product = repoProduct.GetBy(x => x.ID == item.ID);
                        SoldProduct soldProduct = new SoldProduct
                        {
                            OrderId = vm.Order.Id,
                            Title = product.Title,
                            ShortDescription = product.ShortDescription,
                            DiscountedPrice = product.DiscountedPrice * item.Quantity,
                            Quantity = item.Quantity,
                            ProductId = item.ID,
                        };
                        soldProducts.Add(soldProduct);
                    }
                    await repoSoldProduct.AddRange(soldProducts);
                }
            }
            return View();
        }
    }
}
