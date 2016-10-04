using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7.Model
{
    public class WebShopRepository : DbContext
    {
        public WebShopRepository(DbContextOptions<WebShopRepository> options) : base(options)
        {

        }

        public WebShopRepository()
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductTranslation> ProductTranslations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductTranslation>()
            .HasKey(c => new { c.ProductId, c.Language });
        }
        public DbSet<Cart>
            Carts{ get; set; }
        public DbSet<Order> 
            Orders{ get; set; }
        public DbSet<OrderDetail>
         OrderDetails       
        { get; set; }

    
    }
}
