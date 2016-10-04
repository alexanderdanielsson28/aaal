using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication7.Interfaces;

namespace WebApplication7.Controllers
{
   
    public class HomeController : Controller
    {
        
        private IDateTime datetime;
        

        public HomeController(IDateTime datetime)
        {
            this.datetime = datetime;
        }

        public IActionResult Index()
        {
            return View();
        }
           

        public IActionResult About()
        {
            ViewData["Message"] = "sdf";

            return View("");
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = datetime.Now;

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
      
    }
}
