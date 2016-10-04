using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication7.Model;

namespace WebApplication7.ViewModels
{
    public class ProductViewModel
    {
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }

        public virtual ICollection<ProductTranslation> Translations { get; set; }
       
        public string Language { get; set; }
       
       
    
    }
}
