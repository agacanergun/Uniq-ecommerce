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
    [Table("SmallSlider", Schema = "dbo")]
    public class SmallSlider
    {
        public int Id { get; set; }

        [StringLength(20), Column(TypeName = "varchar(20)"), Required(), Display(Name = "İlk Başlık")]
        public string FirstTitle { get; set; }
        [StringLength(35), Column(TypeName = "varchar(35)"), Required(), Display(Name = "İlk Başlık Açıklama")]
        public string FirstDescription { get; set; }
        [StringLength(20), Column(TypeName = "varchar(20)"), Required(), Display(Name = "İkinci Başlık")]
        public string SecondTitle { get; set; }
        [StringLength(35), Column(TypeName = "varchar(35)"), Required(), Display(Name = "İkinci Başlık Açıklama")]
        public string SecondDescription { get; set; }
        [StringLength(20), Column(TypeName = "varchar(20)"), Required(), Display(Name = "Üçüncü Başlık")]
        public string ThirdTitle { get; set; }
        [StringLength(35), Column(TypeName = "varchar(35)"), Required(), Display(Name = "Üçüncü Başlık Açıklama")]
        public string ThirdDescription { get; set; }
        [StringLength(20), Column(TypeName = "varchar(20)"), Required(), Display(Name = "Dördüncü Başlık")]
        public string FourthTitle { get; set; }
        [StringLength(35), Column(TypeName = "varchar(35)"), Required(), Display(Name = "Dördüncü Başlık Açıklama")]
        public string FourthDescription { get; set; }
    }
}
