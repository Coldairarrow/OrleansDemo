using EFCore.Sharding;

namespace StockAccess
{
    public class CustomContext : GenericDbContext
    {
        public CustomContext(GenericDbContext dbContext) 
            : base(dbContext)
        {

        }
    }
}
