using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swap.Api.Models;
using Swap.Api.Service;
//using Swap.Api.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Swap.Api.Controllers
{
	[Authorize]
	[Route("api/item")]
	[ApiController]
	public class ItemController : ControllerBase
	{
		private readonly IItemService _itemService;

		public ItemController(IItemService itemService)
		{
			_itemService = itemService;
		}
		// GET api/values
		[HttpGet]
		public ActionResult<IEnumerable<string>> Get()
		{
			var itens = _itemService.Get();
			return Ok(itens);
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public ActionResult<string> Get(int id)
		{
			var item = _itemService.Get(item_ => item_.Id == id);
			return Ok(item);
		}

		// POST api/values
		[HttpPost]
		public void Post([FromBody] Item item)
		{
			_itemService.Add(item);
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] Item item)
		{
			_itemService.Update(item);
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			var item = _itemService.Get(e => e.Id == id).First();
			_itemService.Delete(item);
		}
	}
}
