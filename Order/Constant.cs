using StockAccess.Entities;
using System;

namespace StockAccess
{
    public static class Constant
    {
        public static readonly Product DefaultProduct = new Product
        {
            Id = Guid.Parse("7e1f54d9-ab72-e583-4375-0565349c3982"),
            Name = "商品",
            TotalStock = 10000
        };
    }
}
