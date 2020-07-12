using Microsoft.EntityFrameworkCore;
namespace back
{
    public class DataContext : DbContext
    {
        public DbSet<Item> Item { get; set; }

        public DbSet<User> User { get; set; }

        public DataContext(DbContextOptions<DataContext> options) :
            base(options)
        {
        }
    }
}