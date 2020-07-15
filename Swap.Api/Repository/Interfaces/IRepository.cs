using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Swap.Api.Data
{
	public interface IRepository<T> where T : class
	{
		IEnumerable<T> Get();
		IEnumerable<T> Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] props);
		void Add(T entity);
		void Delete(T entity);
		void Update(T entity);
	}
}
