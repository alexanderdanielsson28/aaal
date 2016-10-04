using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication7.Model;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace WebApplication7.Model
{
    
    public class Product
    {
        [Display(Name = "Product Id")]
        [Key]
        public int ProductId { get; set; }

        [Required]
       //regex för antal tilllåna tecken
        [Display(Name = "Product name")]
        public string ProductName { get;  set ;}

        [Range(1, Double.PositiveInfinity, ErrorMessage = "The price must be higher than 0") ]
        [Required]
        public decimal Price { get; set; }

        [Display(Name = "Product Category Id")]
        public int ProductCategoryId { get; set; }

        [Display(Name = "Product Category")]
        public ProductCategory ProductCategory { get; set; }

       
    //    public virtual ICollection<ProductTranslation> Translations { get; set; }
    }
}
public class ProductTranslation
{
    //Here we store properties changing with language
    public int ProductId { get; set; }
    public string Language { get; set; }
    [Required]
    public string ProductName { get; set; }
    [Required]
    public string ProductDescription { get; set; }
}
