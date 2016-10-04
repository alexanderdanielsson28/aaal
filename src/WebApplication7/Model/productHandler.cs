using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication7;
using WebApplication7.Model;
using System.Globalization;
using WebApplication7.ViewModels;
using System.Collections;

namespace WebApplication7.Model
{
    public class productHandler
    {
        public  WebShopRepository _context;

        public productHandler(WebShopRepository context)
        {
            _context = context;
        }
        public async  Task<List<ProductViewModel>> GetAll()
        {
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on
                               new { p.ProductId, Second = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName }
                               equals new { pt.ProductId, Second = pt.Language }
                        select new ViewModels.ProductViewModel
                        {
                            Price = p.Price,
                            ProductId = p.ProductId,
                            ProductName = pt.ProductName,
                            ProductDescription = pt.ProductDescription,
                            ProductCategoryId = p.ProductCategoryId,
                            ProductCategory = p.ProductCategory
                        };
           
            // var list = query.Select(s => new { s.Language, s.Price, s.ProductCategory, s.ProductCategoryId, s.ProductDescription, s.ProductId, s.ProductName, s.Translations }).ToList();
            return (await query.ToListAsync());


        }

       

        public async Task<List<ProductViewModel>> _getById(string _findId)
        {
            int convertedId=int.Parse(_findId);
            var query = from p in _context.Products
                        where p.ProductId== convertedId
                        join pt in _context.ProductTranslations on
                               new { p.ProductId, Second = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName }
                               equals new { pt.ProductId, Second = pt.Language }
                        select new ViewModels.ProductViewModel
                        {
                            Price = p.Price,
                            ProductId = p.ProductId,
                            ProductName = pt.ProductName,
                            ProductDescription = pt.ProductDescription,
                            ProductCategoryId = p.ProductCategoryId,
                            ProductCategory = p.ProductCategory
                        };

            //  ProductViewModel ff = new ProductViewModel();
            //  var r = ff.ProductId;
            return await(query.ToListAsync());

        }

       
    }
   
}


