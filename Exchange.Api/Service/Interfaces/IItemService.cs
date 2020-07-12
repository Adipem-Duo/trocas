using Exchange.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Exchange.Api.Service.Interfaces
{
	public interface IItemService
	{
		IEnumerable<Item> Get();
		IEnumerable<Item> Get(Expression<Func<Item, bool>> predicate, params Expression<Func<Item, object>>[] props);
		void Add(Item entity);
		void Delete(Item entity);
		void Update(Item entity);
	}
}
