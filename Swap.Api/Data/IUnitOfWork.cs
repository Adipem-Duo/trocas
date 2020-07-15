using Swap.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Swap.Api.Data
{
	public interface IUnitOfWork : IDisposable
	{
		DataContext Context { get; }
		void Commit();

	}
}
