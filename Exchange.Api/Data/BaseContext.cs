using Exchange.Api.Models;
using ExchangeT = Exchange.Api.Models.Exchange;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exchange.Api.Data
{
	public class BaseContext : DbContext 
	{
		public virtual DbSet<Item> Itens { get; set; }
		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<ExchangeT> Exchanges { get; set; }

	}
}
