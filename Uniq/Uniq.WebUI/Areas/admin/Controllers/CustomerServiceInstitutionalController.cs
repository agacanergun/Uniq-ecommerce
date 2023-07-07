using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Uniq.BL.Repositories;
using Uniq.DAL.Entities;

namespace Uniq.WebUI.Areas.admin.Controllers
{
    [Area("admin"), Authorize(AuthenticationSchemes = "UniqAdminAuth")]
    public class CustomerServiceInstitutionalController : Controller
    {
        IRepository<CustomerServiceInstitutional> repoCustomerServiceInstitutional;
        public CustomerServiceInstitutionalController(IRepository<CustomerServiceInstitutional> repoCustomerServiceInstitutional)
        {
            this.repoCustomerServiceInstitutional = repoCustomerServiceInstitutional;
        }
        [Route("admin/musterihizmetlerikurumsal")]
        public IActionResult Index()
        {
            var response = repoCustomerServiceInstitutional.GetAll().OrderBy(x => x.DisplayIndex).ToList();
            return View(response);
        }
        [Route("admin/musterihizmetlerikurumsal/ekle")]
        public IActionResult Add()
        {
            return View();
        }
        [Route("admin/musterihizmetlerikurumsal/ekle"), HttpPost]
        public async Task<IActionResult> Add(CustomerServiceInstitutional CustomerServiceInstitutional)
        {
            if (ModelState.IsValid)
            {
                await repoCustomerServiceInstitutional.Add(CustomerServiceInstitutional);
                return Redirect("/admin/musterihizmetlerikurumsal");
            }
            ViewBag.Error = "Ekleme İşlemi Başarısız";
            return View(CustomerServiceInstitutional);
        }
        [Route("admin/musterihizmetlerikurumsal/guncelle")]
        public IActionResult Update(int id)
        {
            return View(repoCustomerServiceInstitutional.GetBy(x => x.ID == id));
        }
        [Route("admin/musterihizmetlerikurumsal/guncelle"), HttpPost]
        public async Task<IActionResult> Update(CustomerServiceInstitutional CustomerServiceInstitutional)
        {
            if (ModelState.IsValid)
            {
                await repoCustomerServiceInstitutional.Update(CustomerServiceInstitutional);
                return Redirect("/admin/musterihizmetlerikurumsal");
            }
            ViewBag.Error = "Güncelleme İşlemi Başarısız";
            return View(CustomerServiceInstitutional);
        }

        [Route("admin/musterihizmetlerikurumsal/sil")]
        public async Task<string> Delete(int id)
        {
            CustomerServiceInstitutional CustomerServiceInstitutional = new CustomerServiceInstitutional
            {
                ID = id,
            };
            await repoCustomerServiceInstitutional.Delete(CustomerServiceInstitutional);
            return "Ok";
        }
    }
}
