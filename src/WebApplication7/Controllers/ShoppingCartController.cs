using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication7.Model;
using WebApplication7.ViewModels;

namespace WebApplication7.Controllers
{
    public class ShoppingCartController : Controller
    {
        
            WebShopRepository storeDB ;
        //
        // GET: /ShoppingCart/

            public ShoppingCartController(WebShopRepository context)
        {
            storeDB = context;
        }
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext,storeDB);

            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            // Return the view
            return View(viewModel);
        }
        //
        // GET: /Store/AddToCart/5
        public ActionResult AddToCart(int id)
        {
            // Retrieve the album from the database
            var addedProduct = storeDB.Products
                .Single(album => album.ProductId == id);

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext, storeDB);

            cart.AddToCart(addedProduct);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }
        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext, storeDB);

            // Get the name of the album to display confirmation
            string productName = storeDB.Carts
                .Single(item => item.RecordId == id).Product.ProductName;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = ViewBag +" has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }
        //
        // GET: /ShoppingCart/CartSummary
       // [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext, storeDB);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
    }
            
        }
    
