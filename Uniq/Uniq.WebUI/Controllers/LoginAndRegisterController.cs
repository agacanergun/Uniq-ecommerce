using Microsoft.AspNetCore.Mvc;
using Uniq.DAL.Entities;
using System;
using System.Net;
using System.Net.Mail;
using Uniq.BL.Repositories;
using Uniq.WebUI.Tools;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Uniq.WebUI.Models;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

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
        public async Task<IActionResult> Index(Customer customer)
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
                    List<Claim> claims = new List<Claim> {

                    new Claim(ClaimTypes.PrimarySid, signCustomer.Id.ToString()),
                    new Claim(ClaimTypes.Name, signCustomer.Name),
                    new Claim("UserGuid", signCustomer.GuidId.ToString()),
                     };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "UniqMemberAuth");
                    await HttpContext.SignInAsync("UniqMemberAuth", new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties() { IsPersistent = true });
                    return Redirect("/");
                }
                else
                {
                    return RedirectToAction("VerifyAccount", "LoginAndRegister", new { email = customer.Email });

                }
            }

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
        public async Task<IActionResult> VerifyAccount(string email)
        {
            VerifyAcc verifyAcc = new VerifyAcc { Email = email };

            var customer = repoCustomer.GetBy(x => x.Email == email);
            Random rnd = new Random();
            customer.VerificationCode = rnd.Next(100000, 999999);
            await repoCustomer.Update(customer);
            GeneralTool.SendMail(customer.Email, "Hesap Aktivasyonu", "Hesap Aktivasyon Kodunuz : " + customer.VerificationCode);

            return View(verifyAcc);
        }

        [Route("/hesabini-dogrula"), HttpPost]
        public async Task<IActionResult> VerifyAccount(VerifyAcc model)
        {
            string code = model.Number1.ToString() + model.Number2.ToString() + model.Number3.ToString() + model.Number4.ToString() + model.Number5.ToString() + model.Number6.ToString();
            var customer = repoCustomer.GetBy(x => x.Email == model.Email);
            if (customer.VerificationCode == int.Parse(code))
            {
                customer.AccountStatus = 1;
                await repoCustomer.Update(customer);
                TempData["Info"] = "Hesabınız Doğrulandı Giriş Yapabilirsiniz.";
                return Redirect("/Giris-Yap");
            }
            else
            {
                TempData["WrongVerify"] = "Doğrulama Kodu Yanlış";
                return View(model);
            }
        }

        [Route("/cikis-yap")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");

        }
    }
}
