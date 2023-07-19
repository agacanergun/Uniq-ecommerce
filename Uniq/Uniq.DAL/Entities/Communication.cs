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
    [Table("Communication", Schema = "dbo")]
    public class Communication
    {
        public int ID { get; set; }

        [StringLength(100), Column(TypeName = "varchar(100)"), Required(), Display(Name = "İletişim Türü")]
        public string Type { get; set; }

        [StringLength(100), Column(TypeName = "varchar(100)"), Required(), Display(Name = "İletişim Bilgisi")]
        public string Info { get; set; }
        [Display(Name = "Görüntülenme Sırası")]
        public int DisplayIndex { get; set; }
    }
}
