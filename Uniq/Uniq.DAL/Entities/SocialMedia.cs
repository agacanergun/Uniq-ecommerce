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
    public class SocialMedia
    {
        public int ID { get; set; }
        [StringLength(100), Column(TypeName = "varchar(100)"), Required(), Display(Name = "İnstagram Hesabı Linki")]
        public string InstagramLink { get; set; }


        [StringLength(100), Column(TypeName = "varchar(100)"), Required(), Display(Name = "Facebook Hesabı Linki")]
        public string FacebookLink { get; set; }


        [StringLength(100), Column(TypeName = "varchar(100)"), Required(), Display(Name = "Twitter Hesabı Linki")]
        public string Twitterink { get; set; }


        [StringLength(100), Column(TypeName = "varchar(100)"), Required(), Display(Name = "Youtube Hesabı Linki")]
        public string YoutubeLink { get; set; }
    }
}
