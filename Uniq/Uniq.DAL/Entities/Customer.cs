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
    public class Customer
    {
        public int Id { get; set; }
        public Guid GuidId { get; set; }

        [StringLength(30), Column(TypeName = "varchar(30)"), Required(), Display(Name = "İsim")]
        public string Name { get; set; }

        [StringLength(30), Column(TypeName = "varchar(30)"), Required(), Display(Name = "Soy İsim")]
        public string Surname { get; set; }

        [StringLength(10), Column(TypeName = "varchar(10)"), Required(), Display(Name = "Telefon Numarası")]
        public string PhoneNo { get; set; }

        [StringLength(40), Column(TypeName = "varchar(40)"), Required(), Display(Name = "E-Posta Adresi")]
        public string Email { get; set; }

        [StringLength(32), Column(TypeName = "varchar(32)"), Required(), Display(Name = "Şifre")]
        public string Password { get; set; }

        public int AccountStatus { get; set; }
        public int VerificationCode { get; set; }

        public IEnumerable<CustomerAdresses> CustomerAdresses { get; set; }
    }
}
