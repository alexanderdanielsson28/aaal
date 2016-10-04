using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication7.Model;
using Xunit;
using System;


namespace WebApplication7.Controllers
{
    public class ProductsControllerTest
    {

        private static DbContextOptions<WebShopRepository> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<WebShopRepository>();
            builder.UseInMemoryDatabase()
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
        [Fact]
        public async Task IndexListAllProducts()
        {
            //Arrange
            // All contexts that share the same service provider will share the same InMemory database
            var options = CreateNewContextOptions();

            // Insert seed data into the database using one instance of the context
            using (var context = new WebShopRepository(options))
            {
                context.Products.Add(new Product { ProductName = "Test Product 1"});
                context.Products.Add(new Product { ProductName = "Test Product 2" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new WebShopRepository(options))
            {
                var service = new ProductsController(context);
                var _product = new Product
                {

                    ProductName = "alex"
                };
                var result = await service.Create(_product);

                //Assert
                var viewResult = Assert.IsType<RedirectToActionResult>(result);
                //var model = Assert.IsAssignableFrom<IEnumerable<Product>>(
                //    viewResult.ViewData.Model);
                //Assert.Equal(2, model.Count());
                //Assert.Equal("Test Product 1", model.ElementAt(0).ProductName);
               
            }
        }


    }
}