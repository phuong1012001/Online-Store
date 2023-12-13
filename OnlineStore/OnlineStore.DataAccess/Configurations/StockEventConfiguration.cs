using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.DataAccess.Entities;

namespace OnlineStore.DataAccess.Configurations
{
    internal class StockEventConfiguration : IEntityTypeConfiguration<StockEvent>
    {
        public void Configure(EntityTypeBuilder<StockEvent> builder)
        {
        }
    }
}
