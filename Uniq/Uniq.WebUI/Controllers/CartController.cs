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
    }
}
