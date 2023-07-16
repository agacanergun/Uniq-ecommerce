using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uniq.BL.Repositories;
using Uniq.DAL.Entities;

namespace Uniq.WebUI.Areas.admin.Controllers
{
    [Area("admin"), Authorize(AuthenticationSchemes = "UniqAdminAuth")]
    public class OrderController : Controller
    {
        IRepository<Order> repoOrder;
        public OrderController(IRepository<Order> repoOrder)
        {
            this.repoOrder = repoOrder;
        }
        [Route("admin/siparisler")]
        public IActionResult Index()
        {
            var response = repoOrder.GetAll(x=>x.OrderStatus!="Sipariş Tamamlandı").Include(x=>x.Customer).Include(x => x.CustomerAdresses).Include(x => x.SoldProducts).ToList();
            return View(response);
        }

        [Route("admin/tamamlanan-siparisler")]
        public IActionResult CompletedOrders()
        {
            var response = repoOrder.GetAll(x=>x.OrderStatus=="Sipariş Tamamlandı").Include(x => x.CustomerAdresses).Include(x => x.SoldProducts).ToList();
            return View(response);
        }

        [Route("admin/Siparis-Durumu-Duzenle")]
        public IActionResult EditOrder(int itemId, string orderStatus)
        {
            return View();
        }
    }
}
