using Microsoft.EntityFrameworkCore;
using OnlineStore.DataAccess.Configurations;

namespace OnlineStore.DataAccess.DbContexts
{
    public class OnlineStoreDbContext : DbContext
    {
        public OnlineStoreDbContext()
        {
        }

        public OnlineStoreDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new OrderDetailConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new ProductImageConfiguration());
            builder.ApplyConfiguration(new StockConfiguration());
            builder.ApplyConfiguration(new StockEventConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
