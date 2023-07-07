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
    public class CustomerServiceInstitutional
    {
        public int ID { get; set; }

        [StringLength(30), Column(TypeName = "varchar(30)"), Required(), Display(Name = "Eklenecek Yer")]
        public string Type { get; set; }

        [StringLength(20), Column(TypeName = "varchar(20)"), Required(), Display(Name = "Başlık")]
        public string Title { get; set; }

        [Required(), Display(Name = "Açıklama")]
        public string Description { get; set; }
        [Display(Name = "Görüntülenme Sırası")]
        public int DisplayIndex { get; set; }
    }
}
