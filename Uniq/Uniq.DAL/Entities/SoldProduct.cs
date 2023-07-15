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
    public class SoldProduct
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [StringLength(50), Column(TypeName = "varchar(30)"), Required(), Display(Name = "Ürünün Başlığı")]
        public string Title { get; set; }

        [StringLength(50), Column(TypeName = "varchar(30)"), Required(), Display(Name = "Ürünün Kısa Açıklaması")]
        public string ShortDescription { get; set; }

        [Column(TypeName = "decimal(18,2)"), Required(), Display(Name = "İndirimli Fiyatı")]
        public decimal DiscountedPrice { get; set; }

        [Display(Name = "Ürün Resimleri")]
        public ICollection<ProductPicture> ProductPictures { get; set; }
    }
}
