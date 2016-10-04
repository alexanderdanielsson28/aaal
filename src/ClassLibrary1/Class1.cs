using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
namespace WebApplication7.Controllers
{
    class StaticDateTime /* IDateTime*/
    {
        public DateTime Now
        {
            get
            {
                return new DateTime(2016, 09, 01, 6, 0, 0);
            }
        }
        public class HomeControllerTest
        {

            //[Fact]
            //public void HomecontrollerContactTest()
            //{   //Arrange
            //    var datetime = new DateTime(2016, 09, 01, 6, 0, 0);
            //    var Controller = new HomeController(datetime);

            //    //Act
            //    var result = Controller.Contact();
            //    //Assert
            //    var viewResult = Assert.IsType<VirtualFileResult>(result);
            //    Assert.Equal(datetime.Now, viewResult.ViewData["Message"]);
            //}
        }
    }
}
