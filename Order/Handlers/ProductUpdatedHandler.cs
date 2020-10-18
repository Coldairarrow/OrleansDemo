using Colder.MessageBus.Abstractions;
using EFCore.Sharding;
using Microsoft.EntityFrameworkCore;
using Orleans;
using StockAccess.Entities;
using StockAccess.IGrains;
using System.Linq;
using System.Threading.Tasks;

namespace StockAccess.Handlers
{
    public class ProductUpdatedHandler : IMessageHandler<Product>
    {
        private readonly IDbAccessor _db;
        private readonly IGrainFactory _grainFactory;
        public ProductUpdatedHandler(IDbAccessor db, IGrainFactory grainFactory)
        {
            _db = db;
            _grainFactory = grainFactory;
        }
        public async Task Handle(IMessageContext<Product> context)
        {
            var data = await _grainFactory.GetGrain<IProduct>(context.Message.Id).GetState();
            var theProduct = await _db.GetIQueryable<Product>().AsTracking()
                .Where(x => x.Id == Constant.DefaultProduct.Id)
                .FirstOrDefaultAsync();
            theProduct.UsedStock = context.Message.UsedStock;

            await _db.SaveChangesAsync();
        }
    }
}
