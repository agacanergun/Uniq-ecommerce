using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Uniq.BL.Repositories;
using Uniq.DAL.Entities;

namespace Uniq.WebUI.Areas.admin.Controllers
{
    [Area("admin"), Authorize(AuthenticationSchemes = "UniqAdminAuth")]
    public class ProductPictureController : Controller
    {

        IRepository<ProductPicture> repoProductPicture;
        IWebHostEnvironment hostingEnvironment;
        public ProductPictureController(IRepository<ProductPicture> _repoProductPicture, IWebHostEnvironment hostingEnvironment)
        {
            repoProductPicture = _repoProductPicture;
            this.hostingEnvironment = hostingEnvironment;
        }

        [Route("/admin/resim")]
        public IActionResult Index(int productid)
        {
            ViewBag.ProductID = productid;
            return View(repoProductPicture.GetAll(x => x.ProductID == productid).ToList());
        }


        [Route("/admin/resim/yeni")]
        public IActionResult Add(int productid)
        {
            ViewBag.ProductID = productid;
            return View();
        }

        [Route("/admin/resim/yeni"), HttpPost]
        public async Task<IActionResult> Add(ProductPicture model)
        {
            if (ModelState.IsValid)
            {
                if (Request.Form.Files.Any())
                {
                    if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "productPicture"))) Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "productPicture"));
                    Random rnd = new Random();
                    string dosyaAdi = (DateTime.Now.Minute + DateTime.Now.Millisecond + rnd.Next(10, 1000)) + Request.Form.Files["Picture"].FileName;
                    using (FileStream stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "productPicture", dosyaAdi), FileMode.Create))//resmin fiziksel kopyası için
                    {
                        await Request.Form.Files["Picture"].CopyToAsync(stream);
                    }
                    model.Picture = "/productPicture/" + dosyaAdi;//db deki veri için
                }
                await repoProductPicture.Add(model);
                return RedirectToAction("Index", "ProductPicture", new { productid = model.ProductID });
            }
            ViewBag.Error = "Ekleme İşlemi Başarısız";
            return View(model);
        }

        [Route("/admin/resim/duzenle")]
        public IActionResult Update(int id)
        {
            return View(repoProductPicture.GetBy(x => x.Id == id));
        }

        [Route("/admin/resim/duzenle"), HttpPost]
        public async Task<IActionResult> Update(ProductPicture model)
        {
            if (ModelState.IsValid)
            {
                if (Request.Form.Files.Any())
                {
                    if (model.Picture != null && model.Picture.Length > 0)
                    {
                        string deleteFilePath = Path.Combine(hostingEnvironment.WebRootPath, model.Picture.TrimStart('/'));
                        if (System.IO.File.Exists(deleteFilePath))
                        {
                            System.IO.File.Delete(deleteFilePath);
                        }
                    }

                    if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "productPicture"))) Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "productPicture"));
                    Random rnd = new Random();
                    string dosyaAdi = (DateTime.Now.Minute + DateTime.Now.Millisecond + rnd.Next(10, 1000)) + Request.Form.Files["Picture"].FileName;
                    using (FileStream stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "productPicture", dosyaAdi), FileMode.Create))//resmin fiziksel kopyası için
                    {
                        await Request.Form.Files["Picture"].CopyToAsync(stream);
                    }
                    model.Picture = "/productPicture/" + dosyaAdi;//db deki veri için
                }
                await repoProductPicture.Update(model);
                return RedirectToAction("Index", "ProductPicture", new { productid = model.ProductID });
            }
            ViewBag.Error = "Güncelleme İşlemi Başarısız";
            return View(model);
        }

        [Route("/admin/resim/sil"), HttpPost]
        public async Task<string> Delete(int id)
        {
            ProductPicture productPicture = repoProductPicture.GetBy(x => x.Id == id) ?? null;

            if (productPicture != null)
            {
                string deleteFilePath = Path.Combine(hostingEnvironment.WebRootPath, productPicture.Picture.TrimStart('/'));
                if (System.IO.File.Exists(deleteFilePath))
                {
                    System.IO.File.Delete(deleteFilePath);
                }

                await repoProductPicture.Delete(productPicture);
            }
            return "Ok";
        }
    }
}
