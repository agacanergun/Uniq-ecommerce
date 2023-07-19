using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniq.DAL.Entities
{
    [Table("Slider", Schema = "dbo")]
    public class Slider
    {
        public int Id { get; set; }
        [StringLength(50), Column(TypeName = "varchar(50)"), Required(), Display(Name = "Ufak Başlık")]
        public string SmallTitle { get; set; }

        [StringLength(50), Column(TypeName = "varchar(50)"), Required(), Display(Name = "Başlık")]
        public string Title { get; set; }
        [StringLength(300), Column(TypeName = "varchar(300)"), Required(), Display(Name = "Açıklama")]
        public string Description { get; set; }
        [Display(Name = "Sol Fotoğraf")]
        public string LeftPhoto { get; set; }
        [Display(Name = "Sağ Fotoğraf")]
        public string RightPhoto { get; set; }
    }
}
