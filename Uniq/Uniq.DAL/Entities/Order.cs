using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Uniq.DAL.Entities
{
    [Table("Order", Schema = "dbo")]
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [StringLength(50), Column(TypeName = "varchar(50)")]
        public string OrderNumber { get; set; }

        public int? CustomerAdressesId { get; set; }
        public CustomerAdresses CustomerAdresses { get; set; }

        [StringLength(20), Column(TypeName = "varchar(20)"), Display(Name = "Sipariş Durumu")]
        public string OrderStatus { get; set; }

        [StringLength(30), Column(TypeName = "varchar(30)"), Display(Name = "Kargo Firmasi")]
        public string ShippingType { get; set; }

        public ICollection<SoldProduct> SoldProducts { get; set; }

        public int Status { get; set; }

        public DateTime OrderDateTime { get; set; }
    }
}
