using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniq.DAL.Entities
{
    public class Product
    {
        public int ID { get; set; }

        [StringLength(50), Column(TypeName = "varchar(30)"), Required(), Display(Name = "Ürünün Başlığı")]
        public string Title { get; set; }

        [StringLength(50), Column(TypeName = "varchar(30)"), Required(), Display(Name = "Ürünün Kısa Açıklaması")]
        public string ShortDescription { get; set; }
        [Required(), Display(Name = "Ürünün Uzun Açıklaması")]
        public string Description { get; set; }

        [Display(Name = "Görüntülenme Sırası"), Required()]
        public int DisplayIndex { get; set; }

        [Column(TypeName = "decimal(18,2)"), Required(), Display(Name = "İndirimsiz Fiyatı")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)"), Required(), Display(Name = "İndirimli Fiyatı")]
        public decimal DiscountedPrice { get; set; }

        [StringLength(3), Column(TypeName = "varchar(3)"), Required(), Display(Name = "Yüzdelik İndirim Oranı")]
        public string DiscountRate { get; set; }

        [Display(Name = "Unique Önerilenlerde Gözüksün"), Required()]
        public int SuggestedUnique { get; set; }

        [Display(Name = "Ana Sayfada Gözüksün"), Required()]
        public int ShowOnMainPage { get; set; }

        [Display(Name = "Ürün Resimleri")]
        public ICollection<ProductPicture> ProductPictures { get; set; }
        [Display(Name = "Ürün Kategorileri")]
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
