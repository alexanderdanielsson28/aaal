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

namespace WebApplication7.Controllers
{
    public class ProductsController : Controller
    {   
        private readonly WebShopRepository _context;

        public ProductsController(WebShopRepository context)
        {
            _context = context;    
        }

        // GET: Products
        public async Task<IActionResult> Index()
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
            return View(await query.ToListAsync());
        }
        //

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.SingleOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "ProductCategoryId", "ProductCategoryId");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Price,ProductCategoryId,ProductName")] Product product)
        {
            if (ModelState.IsValid)
            {

                _context.Add(product);
              
                await _context.SaveChangesAsync();
                // return RedirectToAction("Index");
                check(product.ProductName);
                return View( "Description");
            }
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "ProductCategoryId", "ProductCategoryId", product.ProductCategoryId);
         
            return View(product);
        }
        [HttpPost]
        public IActionResult check(string product)
        {
            var query = (
            from p in _context.Products

            where product == p.ProductName
            //select p.ProductId);
            select new ViewModels.ProductViewModel
            {

                ProductId = p.ProductId,

            });


            return ValidSearch(query);

            //      select new ViewModels.ProductViewModel { ProductId = p.ProductId, };

            //ViewData["EmployeeName"] = await query;

        }

        private IActionResult ValidSearch(IQueryable<ProductViewModel> query)
        {
            var qsList = query.ToArray();
            int array=0;
            foreach (var i in qsList)
            {
                 array = qsList[0].ProductId;
            };


        
            //   List<int> prodClist = new List<int>();

            //ViewBag.LinkableId = fuck;
            ViewBag.LinkableId = (array.ToString()
                );

            return View("Description");
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTrans([Bind("ProductId,Language,ProductName,ProductDescription")] ProductTranslation product)
        {
            

            if (ModelState.IsValid)
            {

                _context.ProductTranslations.AddRange(
                    new ProductTranslation
                    {
                        ProductId = product.ProductId,
                        Language = product.Language,
                        ProductDescription = product.ProductDescription,
                        ProductName = product.ProductName
                    });
                await _context.SaveChangesAsync();
                return View("Description");
            }
            return View(product);
        }

        private int getID()
        {
            throw new NotImplementedException();
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.SingleOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "ProductCategoryId", "ProductCategoryId", product.ProductCategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Price,ProductCategoryId,ProductName")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "ProductCategoryId", "ProductCategoryId", product.ProductCategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.SingleOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(m => m.ProductId == id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
