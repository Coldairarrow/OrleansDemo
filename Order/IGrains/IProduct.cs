using Orleans;
using StockAccess.Entities;
using System.Threading.Tasks;

namespace StockAccess.IGrains
{
    interface IProduct : IGrainWithGuidKey
    {
        Task<bool> UseStock(int count);
        Task<Product> GetState();
    }
}
