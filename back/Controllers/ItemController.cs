using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace back
{
    [ApiController]
    [Route("api/item")]
    public class ItemController : Controller
    {
        [HttpGet]
        public ActionResult<Item> Get()
        {
            var item = new Item();
            item.Name = "Rolha";
            return item;
        }
    }
}