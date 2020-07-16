using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Swap.Api.Models;

namespace Swap.Api.Data
{
	public class DataContext : IdentityDbContext<ApplicationUser> 
	{
		public virtual DbSet<Item> Itens { get; set; }
		public virtual DbSet<Exchange> Exchanges { get; set; }

		public DataContext(DbContextOptions<DataContext> options)
			: base(options)
		{
			
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) 
		{
			base.OnModelCreating(modelBuilder);
			var item = modelBuilder.Entity<Item>();
			item.HasKey(e => e.Id);
			item.Property(b => b.Id)
				 .HasIdentityOptions(startValue: 1, incrementBy: 1, numbersToCache: 30);

			//modelBuilder.Entity<User>().Property(b => b.Id)
			//	 .HasIdentityOptions(startValue: 1, incrementBy: 1, numbersToCache: 30);

			var exchange = modelBuilder.Entity<Exchange>();
			exchange.HasKey(e => e.Id);
			exchange.Property(b => b.Id)
				 .HasIdentityOptions(startValue: 1,incrementBy: 1,numbersToCache:30);

		}
	}
}
