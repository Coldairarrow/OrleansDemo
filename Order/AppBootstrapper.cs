using EFCore.Sharding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StockAccess.Entities;
using System;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace StockAccess
{
    public class AppBootstrapper : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        public AppBootstrapper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scop = _serviceProvider.CreateScope();
            var db = scop.ServiceProvider.GetService<IDbAccessor>();
            var theProduct = await db.GetIQueryable<Product>().AsTracking()
                .Where(x => x.Id == Constant.DefaultProduct.Id)
                .FirstOrDefaultAsync();

            theProduct.UsedStock = 0;
            await db.SaveChangesAsync();
        }
    }
}
