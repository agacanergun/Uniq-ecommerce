using Microsoft.AspNetCore.Mvc;
using Uniq.DAL.Entities;
using System;
using System.Net;
using System.Net.Mail;
using Uniq.BL.Repositories;
using Uniq.WebUI.Tools;

namespace Uniq.WebUI.Controllers
{
    public class LoginAndRegisterController : Controller
    {
        IRepository<Customer> repoCustomer;
        public LoginAndRegisterController(IRepository<Customer> repoCustomer)
        {
            this.repoCustomer = repoCustomer;
        }

        [Route("/Giris-Yap")]
        public IActionResult Index()
        {
            return View();
        }

        //[Route("/Giris-Yap"),HttpPost]
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [Route("/Kayıt-Ol")]
        public IActionResult Register()
        {
            return View();
        }


        [Route("/Kayıt-Ol"), HttpPost]
        public IActionResult Register(Customer customer)
        {
            var checkCustomer = repoCustomer.GetBy(x => x.Email == customer.Email);
            if (checkCustomer == null)
            {
                //Random rnd = new Random();
                //customer.GuidId = Guid.NewGuid();
                //customer.AccountStatus = 0;
                //customer.VerificationCode = rnd.Next(100000, 999999);
                //customer.Password = GeneralTool.getMD5(customer.Password);
                //repoCustomer.Add(customer);

                TempData["Info"] = "Kayıt Oluşturuldu Hesabınıza Giriş Yapabilirsiniz.";
                return Redirect("/Giris-Yap");
            }
            else
            {
                //Bu Email Adresi Zaten Kayıtlı
                return View(customer);
            }
        }
    }
}
