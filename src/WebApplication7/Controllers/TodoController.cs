using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication7.Model;
using Microsoft.DotNet.Cli.Utils.CommandParsing;
using System.Collections;
using WebApplication7.ViewModels;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication7.Controllers
{
   
    [Route("api/[Controller]")]
        public class TodoController : Controller
        {
            public TodoController(WebShopRepository todoItems)
            {
                TodoItems = todoItems;
            }
            public WebShopRepository TodoItems { get; set; }
    

    [HttpGet]
    public   Task<List<ProductViewModel>> GetAll()
    {

            productHandler test = new productHandler(TodoItems);
           var list= test.GetAll();
            return list;
    }


    [HttpGet("{id}", Name = "GetTodo")]
    public IActionResult GetById(string id)
        {
            productHandler test = new productHandler(TodoItems);
            var item = test._getById(id);
           
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item.ToString());
        }


        [HttpPost]
        public IActionResult Create([FromBody] Product item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            TodoItems.Add(item);
            TodoItems.SaveChanges();
            return CreatedAtRoute("GetTodo", new { id = item.ProductId }, item);
        }
       


    }
}

