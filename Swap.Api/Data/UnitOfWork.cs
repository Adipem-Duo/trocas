using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swap.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Swap.Api.Data
{
	public class UnitOfWork : IUnitOfWork
	{
		public DataContext Context { get; }

		public UnitOfWork(DataContext context)
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
