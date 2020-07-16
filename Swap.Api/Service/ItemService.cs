using Swap.Api.Data;
using Swap.Api.Models;
using Swap.Api.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Swap.Api.Service
{
	public interface IItemService
	{
		IEnumerable<Item> Get();
		IEnumerable<Item> Get(Expression<Func<Item, bool>> predicate, params Expression<Func<Item, object>>[] props);
		void Add(Item entity);
		void Delete(Item entity);
		void Update(Item entity);
	}

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
			_repo.Add(entity);
			_uow.Commit();
		}

		public void Delete(Item entity)
		{
			_repo.Delete(entity);
			_uow.Commit();
		}

		public IEnumerable<Item> Get()
		{
			return _repo.Get();
		}

		public IEnumerable<Item> Get(Expression<Func<Item, bool>> predicate, params Expression<Func<Item, object>>[] props)
		{
			return _repo.Get(predicate, props);
		}


		public void Update(Item entity)
		{
			_repo.Update(entity);
			_uow.Commit();

		}
	}
}
