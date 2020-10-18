using Colder.MessageBus.Abstractions;
using EFCore.Sharding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Orleans;
using StockAccess.Entities;
using StockAccess.IGrains;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StockAccess.Grains
{
    public class ProductGrain : Grain<Product>, IProduct
    {
        private readonly IMessageBus _messageBus;
        public ProductGrain(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }
        public Task<Product> GetState()
        {
            return Task.FromResult(State);
        }

        public async Task<bool> UseStock(int count)
        {
            if (State.LastStock >= count)
            {
                State.UsedStock += count;
                await WriteStateAsync();

                return true;
            }
            else
            {
                return false;
            }
        }

        protected override async Task ReadStateAsync()
        {
            await base.ReadStateAsync();

            if (State.Id == Guid.Empty)
            {
                using var scop = ServiceProvider.CreateScope();
                var db = scop.ServiceProvider.GetService<IDbAccessor>();
                var theProduct = await db.GetIQueryable<Product>()
                    .Where(x => x.Id == Constant.DefaultProduct.Id)
                    .FirstOrDefaultAsync();

                State = theProduct;
            }
        }

        protected override async Task WriteStateAsync()
        {
            await base.WriteStateAsync();

            //交给消息队列处理，异步写数据库
            await _messageBus.Publish(State);
        }
    }
}
