using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Uniq.BL.Repositories;
using Uniq.DAL.Entities;
using Uniq.WebUI.Models;

namespace Uniq.WebUI.Controllers
{
    public class CartController : Controller
    {
        IRepository<Product> repoProduct;
        public CartController(IRepository<Product> repoProduct)
        {
            this.repoProduct = repoProduct;
        }


        [Route("/sepetim")]
        public IActionResult Index()
        {
            if (Request.Cookies["MyCart"] != null)
            {
                List<Cart> carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]);
                if (carts.Count() == 0)
                    return Redirect("/");
                else
                    return View(carts);
            }
            else return Redirect("/");
        }

        [Route("/sepetim/ekle")]
        public string AddCart(int productid, int quantity)
        {
            Product product = repoProduct.GetAll(x => x.ID == productid).Include(x => x.ProductPictures).FirstOrDefault() ?? null;
            if (product != null)//sepete ekleme işlemleri
            {
                Cart cart = new Cart
                {
                    ID = product.ID,
                    Name = product.Title,
                    Picture = product.ProductPictures.Any() ? product.ProductPictures.FirstOrDefault().Picture : "/assetsAdmin/dist/images/gorselhazirlaniyor.jpg",
                    Price = product.DiscountedPrice,
                    Quantity = quantity
                };
                List<Cart> carts = new List<Cart>();
                bool urunVarmi = false;
                if (Request.Cookies["MyCart"] != null)//daha önce sepete eklenmiş bir ürün varsa
                {
                    carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]);

                    foreach (Cart _cart in carts)
                    {
                        if (_cart.ID == productid)
                        {
                            urunVarmi = true;
                            _cart.Quantity += quantity;
                            break;
                        }
                    }
                }
                if (urunVarmi == false) carts.Add(cart);
                CookieOptions cookieOptions = new();
                cookieOptions.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Append("MyCart", JsonConvert.SerializeObject(carts), cookieOptions);
                return product.Title;
            }
            else return "";
        }


        [Route("/sepetim/sayiver")]
        public int GetCartCount()
        {
            if (Request.Cookies["MyCart"] != null)
            {
                return JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]).Sum(x => x.Quantity);
            }
            else return 0;
        }

        [Route("/sepetim/sil")]
        public string RemoveCart(int productid)
        {
            if (Request.Cookies["MyCart"] != null)
            {
                List<Cart> carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]);
                carts.Remove(carts.FirstOrDefault(x => x.ID == productid));
                CookieOptions cookieOptions = new();
                cookieOptions.Expires = DateTime.Now.AddDays(3);
                Response.Cookies.Append("MyCart", JsonConvert.SerializeObject(carts), cookieOptions);
                return "OK";
            }
            else return "";
        }


        [Route("sepetim/arttir")]
        public int PlusQuantity(int productid)
        {
            var carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]);
            foreach (var item in carts)
            {
                if (item.ID == productid)
                {
                    item.Quantity++;
                    CookieOptions cookieOptions = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(3),
                    };
                    Response.Cookies.Append("MyCart", JsonConvert.SerializeObject(carts), cookieOptions);
                    return item.Quantity;
                }
            }
            return -1;
        }

        [Route("sepetim/azalt")]
        public int MinusQuantity(int productid)
        {
            var carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["MyCart"]);
            foreach (var item in carts)
            {
                if (item.ID == productid)
                {
                    item.Quantity--;
                    if (item.Quantity == 0)
                    {
                        carts.Remove(item);
                        //CookieOptions cookieOptions1 = new CookieOptions
                        //{
                        //    Expires = DateTime.Now.AddDays(3),
                        //};
                        //Response.Cookies.Append("MyCart", JsonConvert.SerializeObject(carts), cookieOptions1);

                    }
                    CookieOptions cookieOptions = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(3),
                    };
                    Response.Cookies.Append("MyCart", JsonConvert.SerializeObject(carts), cookieOptions);
                    return item.Quantity;
                }
            }
            return -1;
        }


        [Route("/sepetim/tamamla"), Authorize(AuthenticationSchemes = "UniqMemberAuth")]
        public IActionResult Complete()
        {
            return View();
        }

        //[Route("/sepetim/tamamla"), Authorize(AuthenticationSchemes = "UniqMemberAuth"), HttpPost]
        //public IActionResult Complete()
        //{
        //    return View();
        //}
    }
}
