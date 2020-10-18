using EFCore.Sharding;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using StockAccess.Entities;
using StockAccess.IGrains;
using System.Threading.Tasks;

namespace StockAccess.Controllers
{
    [Route("product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IDbAccessor _db;
        private readonly IGrainFactory _grainFactory;
        public ProductController(IDbAccessor db, IGrainFactory grainFactory)
        {
            _db = db;
            _grainFactory = grainFactory;
        }

        [Route("db")]
        [HttpGet]
        public async Task<string> UseStockByDb()
        {
            await _db.UpdateSqlAsync<Product>(x => x.Id == Constant.DefaultProduct.Id && x.TotalStock - x.UsedStock >= 1,
                (nameof(Product.UsedStock), UpdateType.Add, 1));

            return "OK";
        }

        [Route("orleans")]
        [HttpGet]
        public async Task<string> UseStockByOrleans()
        {
            await _grainFactory.GetGrain<IProduct>(Constant.DefaultProduct.Id).UseStock(1);

            return "OK";
        }
    }
}
