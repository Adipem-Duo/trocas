using Exchange.Api.Data;
using Exchange.Api.Models;
using Exchange.Api.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Exchange.Api.Service
{
	public class ItemService : IItemService
	{
		private readonly IUnitOfWork _uow;
		private readonly IRepository<Item> _repo;
		public  ItemService(IUnitOfWork uow, IRepository<Item> repo)
		{
			_uow = uow;
			_repo = repo;
		}

		public void Add(Item entity)
		{
			
		}

		public void Delete(Item entity)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Item> Get()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Item> Get(Expression<Func<Item, bool>> predicate, params Expression<Func<Item, object>>[] props)
		{
			throw new NotImplementedException();
		}

		public void Insert(Item item)
		{
			

		}

		public void Update(Item entity)
		{
			throw new NotImplementedException();
		}
	}
}
