﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniq.DAL.Entities
{
    [Table("Admin", Schema ="dbo")]
    public class Admin
    {
        public int ID { get; set; }

        [StringLength(30), Column(TypeName = "varchar(30)"), Required(), Display(Name = "Yetkili Adı")]
        public string Name { get; set; }

        [StringLength(30), Column(TypeName = "varchar(30)"), Required(), Display(Name = "Yetkili Soyadı")]
        public string Surname { get; set; }

        [StringLength(30), Column(TypeName = "varchar(30)"), Required(), Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [StringLength(32), Column(TypeName = "varchar(32)"), Required(), Display(Name = "Şifre")]
        public string Password { get; set; }
    }
}
