using Exchange.Api.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Exchange.Api.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly IUnitOfWork _unitOfWork;
		public Repository(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public void Add(T entity)
		{
			_unitOfWork.Context.Set<T>().Add(entity);
		}

		public void Delete(T entity)
		{
			T existing = _unitOfWork.Context.Set<T>().Find(entity);
			if (existing != null) _unitOfWork.Context.Set<T>().Remove(existing);
		}

		public IEnumerable<T> Get()
		{
			return _unitOfWork.Context.Set<T>().AsEnumerable<T>();
		}

		public IEnumerable<T> Get(Expression<Func<T, bool>> predicate, params Expression<Func<T,object>>[] props)
		{
			var query = _unitOfWork.Context.Set<T>().AsQueryable();
			foreach (var prop in props)
				query = query.Include(prop);
			query = query.Where(predicate);

			return query.ToList();
		}

		public void Update(T entity)
		{
			_unitOfWork.Context.Entry(entity).State = EntityState.Modified;
			_unitOfWork.Context.Set<T>().Attach(entity);
		}
	}
}
