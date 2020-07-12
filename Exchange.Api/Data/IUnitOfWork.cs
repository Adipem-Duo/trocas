using Exchange.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace Exchange.Api.Data
{
	public interface IUnitOfWork : IDisposable
	{
		DbContext Context { get; }
		void Commit();

	}
}
