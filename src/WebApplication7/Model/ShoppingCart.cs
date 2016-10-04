using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace WebApplication7.Model
{
    public partial class ShoppingCart
    {

        WebShopRepository storeDB;

        public ShoppingCart(WebShopRepository ws)
        {
            storeDB=ws;
        }
        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";

        public static ShoppingCart GetCart(HttpContext context, WebShopRepository repo)
        {
            var cart = new ShoppingCart(repo);
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }
      //  Helper method to simplify shopping cart calls
        //    public static ShoppingCart GetCart(Controller controller)
        //{
        //    return GetCart(controller.HttpContext);
        //}
        public void AddToCart(Product product)
        {
         //   Get the matching cart and album instances
           var cartItem = storeDB.Carts.SingleOrDefault(
               c => c.CartId == ShoppingCartId
               && c.ProductID == product.ProductId);

            if (cartItem == null)
            {
            //    Create a new cart item if no cart item exists
               cartItem = new Cart
               {
                   ProductID = product.ProductId,
                   CartId = ShoppingCartId,
                   Count = 1,
                   DateCreated = DateTime.Now
               };
                storeDB.Carts.Add(cartItem);
            }
            else
            {
           //     If the item does exist in the cart,
                //then add one to the quantity
                    cartItem.Count++;
            }
        //    Save changes
                storeDB.SaveChanges();
        }
        public int RemoveFromCart(int id)
        {
        //    Get the cart
           var cartItem = storeDB.Carts.Single(
               cart => cart.CartId == ShoppingCartId
               && cart.RecordId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    storeDB.Carts.Remove(cartItem);
                }
             //   Save changes
                    storeDB.SaveChanges();
            }
            return itemCount;
        }
        public void EmptyCart()
        {
            var cartItems = storeDB.Carts.Where(
                cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                storeDB.Carts.Remove(cartItem);
            }
           // Save changes
                storeDB.SaveChanges();
        }
        public List<Cart> GetCartItems()
        {
            return storeDB.Carts.Where(
                cart => cart.CartId == ShoppingCartId).Include(q=>q.Product).ToList();
        }
        public int GetCount()
        {
        //    Get the count of each item in the cart and sum them up
                int? count = (from cartItems in storeDB.Carts
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Count).Sum();
      //      Return 0 if all entries are null
                return count ?? 0;
        }
        public decimal GetTotal()
        {
            //Multiply album price by count of that album to get
            //     the current price for each of those albums in the cart
            //     sum all album price totals to get the cart total
                decimal ? total = (from cartItems in storeDB.Carts
                                   where cartItems.CartId == ShoppingCartId
                                   select (int?)cartItems.Count *
                                   cartItems.Product.Price).Sum();

            return total ?? decimal.Zero;
        }
        //public int CreateOrder(Order order)
        //{
        //    decimal orderTotal = 0;

        //    var cartItems = GetCartItems();
        //    //Iterate over the items in the cart,
        //    //adding the order details for each
        //        foreach (var item in cartItems)
        //        {
        //            var orderDetail = new OrderDetail
        //            {
        //                AlbumId = item.ProductID,
        //                OrderId = order.OrderId,
        //                UnitPrice = item.Product.Price,
        //                Quantity = item.Count
        //            };
        //            //Set the order total of the shopping cart
        //            orderTotal += (item.Count * item.Product.Price);

        //            storeDB.OrderDetails.Add(orderDetail);

        //        }
        //    //Set the order's total to the orderTotal count
        //        order.Total = orderTotal;

        //    //Save the order
        //        storeDB.SaveChanges();
        //    //Empty the shopping cart
        //        EmptyCart();
        //    //Return the OrderId as the confirmation number
        //        return order.OrderId;
        //}
        //We're using HttpContextBase to allow access to cookies.
            public string GetCartId(HttpContext context)
        {
            string safevalue;
            //Kolla om det kommit in en cookie med namn cart?
            if (context.Request.Cookies["cart"] != null)
            {
                var value = context.Request.Cookies["cart"];
                //Applicera en HtmlEncode för att inte riskera få in
                //tex javascriptkod till sidan.
                safevalue = HtmlEncoder.Default.Encode(value);
            }
            else
            {
                // Generate a new random GUID using System.Guid class
                safevalue = Guid.NewGuid().ToString();
            }
            context.Response.Cookies.Append(
                "cart",
                safevalue,
                new Microsoft.AspNetCore.Http.CookieOptions()
                {
                    Path = "/",
                    HttpOnly = false,  //True == client js can't read.
                    Secure = false,     //True == only https
                    Expires = DateTime.Now.AddMonths(6)  //Cookien sparas i 6 månader framåt.  DateTime.Now.AddDays(-1) tar bort cookien.
                }
            );
            return safevalue;
        }
        //When a user has logged in, migrate their shopping cart to
        //be associated with their username
            public void MigrateCart(string userName)
        {
            var shoppingCart = storeDB.Carts.Where(
                c => c.CartId == ShoppingCartId);

            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }
            storeDB.SaveChanges();
        }
    }
}


