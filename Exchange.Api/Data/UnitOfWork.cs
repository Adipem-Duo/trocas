using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exchange.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Exchange.Api.Data
{
	public class UnitOfWork : IUnitOfWork
	{
		public DbContext Context { get; }

		public UnitOfWork(DbContext context)
		{
			Context = context;
		}
		public void Commit()
		{
			Context.SaveChanges();
		}

		public void Dispose()
		{
			Context.Dispose();

		}
	}
}
