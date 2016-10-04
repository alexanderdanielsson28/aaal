using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7.Model
{
    public class ProductCategory
    {
        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }

        public ICollection<Product> Product { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
    }
}
