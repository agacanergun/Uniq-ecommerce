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
    public class CustomerAdresses
    {
        public int Id { get; set; }

        public int CustomerID { get; set; }
        public Customer Customer { get; set; }

        [StringLength(25), Column(TypeName = "varchar(25)"), Required(), Display(Name = "Adres Notu")]
        public string Title { get; set; }

        [StringLength(20), Column(TypeName = "varchar(20)"), Required(), Display(Name = "Ülke")]
        public string Country { get; set; }

        [StringLength(20), Column(TypeName = "varchar(20)"), Required(), Display(Name = "İl")]
        public string Province { get; set; }

        [StringLength(20), Column(TypeName = "varchar(20)"), Required(), Display(Name = "İlçe")]
        public string District { get; set; }

        [StringLength(100), Column(TypeName = "varchar(100)"), Required(), Display(Name = "Açık Adres Bilgisi")]
        public string Adress { get; set; }
    }
}
