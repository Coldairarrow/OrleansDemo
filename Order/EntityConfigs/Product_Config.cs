using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockAccess.Entities;

namespace StockAccess.EntityConfigs
{
    public class Product_Config : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(Constant.DefaultProduct);
        }
    }
}
