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
    public class Shipping
    {
        public int ID { get; set; }

        [StringLength(30), Column(TypeName = "varchar(30)"), Required(), Display(Name = "Kargo Adı")]
        public string Name { get; set; }
        [Display(Name = "Görüntülenme Sırası")]
        public int DisplayIndex { get; set; }
    }
}
