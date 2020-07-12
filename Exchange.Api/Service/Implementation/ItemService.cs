using Exchange.Api.Data;
using Exchange.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exchange.Api.Service
{
	public class ItemService
	{
		private readonly IUnitOfWork _uow;
		public  ItemService(IUnitOfWork uow)
		{
			_uow = uow;
		}
		public void Insert(Item item)
		{
			_uow.GetRepository<Item>().Add(item);

		}

	}
}
