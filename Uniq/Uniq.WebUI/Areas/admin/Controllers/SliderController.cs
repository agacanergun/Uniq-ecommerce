using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Uniq.BL.Repositories;
using Uniq.DAL.Entities;

namespace Uniq.WebUI.Areas.admin.Controllers
{
    [Area("admin"), Authorize(AuthenticationSchemes = "UniqAdminAuth")]
    public class SliderController : Controller
    {
        IRepository<Slider> repoSlider;
        IWebHostEnvironment hostingEnvironment;
        public SliderController(IRepository<Slider> repoSlider, IWebHostEnvironment hostingEnvironment)
        {
            this.repoSlider = repoSlider;
            this.hostingEnvironment = hostingEnvironment;

        }
        [Route("admin/slayt")]
        public IActionResult Index()
        {
            var response = repoSlider.GetAll().ToList();
            return View(response);
        }

        [Route("admin/slayt/ekle")]
        public IActionResult Add()
        {
            return View();
        }
        [Route("admin/slayt/ekle"), HttpPost]
        public async Task<IActionResult> Add(Slider model)
        {
            var slider = repoSlider.GetAll().FirstOrDefault();
            if (slider != null)
            {
                ViewBag.Error = "Bu Alanda Yanlızca 1 Adet Veri Ekleyebilirsiniz.";
                return View(model);
            }

            if (ModelState.IsValid)
            {
                if (Request.Form.Files.Any())
                {
                    for (int i = 0; i < Request.Form.Files.Count; i++)
                    {
                        if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "slider"))) Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "slider"));
                        Random rnd = new Random();
                        string dosyaAdi = (DateTime.Now.Minute + DateTime.Now.Millisecond + rnd.Next(10, 1000)) + Request.Form.Files[i].FileName;
                        using (FileStream stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "slider", dosyaAdi), FileMode.Create))//resmin fiziksel kopyası için
                        {
                            await Request.Form.Files[i].CopyToAsync(stream);
                        }
                        if (i == 0)
                        {
                            model.LeftPhoto = "/slider/" + dosyaAdi;
                        }
                        else if (i == 1)
                        {
                            model.RightPhoto = "/slider/" + dosyaAdi;
                        }
                    }
                }
                await repoSlider.Add(model);
                return RedirectToAction("Index");
            }
            ViewBag.Error = "Ekleme İşlemi Başarısız";
            return View(model);
        }

        [Route("admin/slayt/guncelle")]
        public IActionResult Update(int id)
        {
            return View(repoSlider.GetBy(x => x.Id == id));
        }
        [Route("admin/slayt/guncelle"), HttpPost]
        public async Task<IActionResult> Update(Slider model)
        {
            if (Request.Form.Files.Any())
            {

                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "slider"))) Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "slider"));
                Random rnd = new Random();
                if (Request.Form.Files["leftphoto"] != null && Request.Form.Files["leftphoto"].FileName.Any())
                {
                    string LeftdosyaAdi = (DateTime.Now.Minute + DateTime.Now.Millisecond + rnd.Next(10, 1000)) + Request.Form.Files["leftphoto"].FileName;
                    using (FileStream stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "slider", LeftdosyaAdi), FileMode.Create))
                    {
                        await Request.Form.Files["leftphoto"].CopyToAsync(stream);
                    }
                    string OldLeftPhotoPath = Path.Combine(hostingEnvironment.WebRootPath, model.LeftPhoto.TrimStart('/'));
                    if (System.IO.File.Exists(OldLeftPhotoPath))
                    {
                        System.IO.File.Delete(OldLeftPhotoPath);
                    }
                    model.LeftPhoto = "/slider/" + LeftdosyaAdi;
                }

                if (Request.Form.Files["rightphoto"] != null && Request.Form.Files["rightphoto"].FileName.Any())
                {
                    string RightdosyaAdi = (DateTime.Now.Minute + DateTime.Now.Millisecond + rnd.Next(10, 1000)) + Request.Form.Files["rightphoto"].FileName;

                    using (FileStream stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "slider", RightdosyaAdi), FileMode.Create))
                    {
                        await Request.Form.Files["rightphoto"].CopyToAsync(stream);
                    }
                    string OldRightPhotoPath = Path.Combine(hostingEnvironment.WebRootPath, model.RightPhoto.TrimStart('/'));
                    if (System.IO.File.Exists(OldRightPhotoPath))
                    {
                        System.IO.File.Delete(OldRightPhotoPath);
                    }
                    model.RightPhoto = "/slider/" + RightdosyaAdi;
                }
            
                await repoSlider.Update(model);
                return RedirectToAction("Index");
            }
            else
            {
                await repoSlider.Update(model);
                return RedirectToAction("Index");
            }
        }

        [Route("admin/slayt/sil")]
        public async Task<string> Delete(int id)
        {
            Slider slider = repoSlider.GetBy(x => x.Id == id) ?? null;

            if (slider != null)
            {
                string LeftPhotoPath = Path.Combine(hostingEnvironment.WebRootPath, slider.LeftPhoto.TrimStart('/'));
                string RightPhotoPath = Path.Combine(hostingEnvironment.WebRootPath, slider.RightPhoto.TrimStart('/'));
                if (System.IO.File.Exists(LeftPhotoPath))
                {
                    System.IO.File.Delete(LeftPhotoPath);
                }
                if (System.IO.File.Exists(RightPhotoPath))
                {
                    System.IO.File.Delete(RightPhotoPath);
                }
                await repoSlider.Delete(slider);
            }
            return "Ok";
        }
    }
}

