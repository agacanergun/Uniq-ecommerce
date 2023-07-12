using Microsoft.AspNetCore.Mvc;
using Uniq.DAL.Entities;
using System;
using System.Net;
using System.Net.Mail;
using Uniq.BL.Repositories;
using Uniq.WebUI.Tools;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Uniq.WebUI.Models;

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

        [Route("/Giris-Yap"), HttpPost]
        public IActionResult Index(Customer customer)
        {
            customer.Password = GeneralTool.getMD5(customer.Password);
            var signCustomer = repoCustomer.GetBy(x => x.Email == customer.Email && x.Password == customer.Password);
            if (signCustomer == null)
            {
                ViewBag.incorrect = "Hatalı Giriş Bilgisi";
                return View();
            }
            else
            {
                if (signCustomer.AccountStatus == 1)
                {
                    //giriş yapabilir
                }
                else
                {
                    return RedirectToAction("VerifyAccount", "LoginAndRegister", new { email = customer.Email });

                }
            }
            return View();
        }

        [Route("/Kayıt-Ol")]
        public IActionResult Register()
        {
            return View();
        }


        [Route("/Kayıt-Ol"), HttpPost]
        public async Task<IActionResult> Register(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var checkCustomer = repoCustomer.GetBy(x => x.Email == customer.Email);
                if (checkCustomer == null)
                {
                    customer.GuidId = Guid.NewGuid();
                    customer.AccountStatus = 0;
                    customer.VerificationCode = 0;
                    customer.Password = GeneralTool.getMD5(customer.Password);
                    await repoCustomer.Add(customer);

                    TempData["Info"] = "Kayıt Oluşturuldu Hesabınıza Giriş Yapabilirsiniz.";
                    return Redirect("/Giris-Yap");
                }
                else
                {
                    //Bu Email Adresi Zaten Kayıtlı
                    ViewBag.FailRegisterEmail = "Bu Email Adresi Zaten Kayıtlı";
                    return View(customer);
                }
            }
            else
            {
                return View(customer);
            }

        }

        [Route("/hesabini-dogrula")]
        public IActionResult VerifyAccount(string email)
        {
            VerifyAcc verifyAcc = new VerifyAcc { Email = email };
            return View(verifyAcc);
        }

        [Route("/hesabini-dogrula"), HttpPost]
        public IActionResult VerifyAccount(VerifyAcc model)
        {
            // veritabanındaki bilgi ile modelden gelen karşılaştırılıp eşleşiyorsa giriş yapma sayfasına yönlendirilecek ve hesabınız aktif edilmiştir başarıyla giriş yapabilrisinz olcak
            return View(model);
        }
    }
}
