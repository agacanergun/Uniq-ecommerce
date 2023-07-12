using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Uniq.DAL.Entities;

namespace Uniq.DAL.Contexts
{
    public class SqlContext : DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {
        }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Communication> Communication { get; set; }
        public DbSet<CustomerServiceInstitutional> CustomerServiceInstitutional { get; set; }
        public DbSet<SocialMedia> SocialMedia { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<ProductPicture> ProductPicture { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerAdresses> CustomerAdresses { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().HasData(new Admin { ID = 1, Name = "ADIM", Surname = "SOYADIM", Password = "4c49a6720254293c040d06f1207d6796", UserName = "uniq" });
            modelBuilder.Entity<ProductCategory>().HasKey(x => new { x.ProductID, x.CategoryID });
            modelBuilder.Entity<ProductPicture>().HasOne(x => x.Product).WithMany(x => x.ProductPictures).HasForeignKey(x => x.ProductID);
            modelBuilder.Entity<Customer>().HasMany(x => x.CustomerAdresses).WithOne(x => x.Customer).HasForeignKey(x => x.CustomerID);
        }
    }
}
