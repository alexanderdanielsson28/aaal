using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication7.Controllers
{
    public class apiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("SinglePage")]
        public IActionResult SinglePage()
        {
            return View("SinglePage");
        }
    }
}