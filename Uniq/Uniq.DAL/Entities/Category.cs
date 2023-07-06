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
    public class Category
    {
        public int ID { get; set; }

        [StringLength(30), Column(TypeName = "varchar(30)"), Required(), Display(Name = "Kategori Adı")]
        public string Name { get; set; }
        public int DisplayIndex { get; set; }
    }
}
