using Colder.MessageBus.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockAccess.Entities
{
    /// <summary>
    /// 商品
    /// </summary>
    [Table(nameof(Product))]
    public class Product : IMessage
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// 商品名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 总库存
        /// </summary>
        public int TotalStock { get; set; }

        /// <summary>
        /// 已用库存
        /// </summary>
        public int UsedStock { get; set; }

        [NotMapped]
        public int LastStock => TotalStock - UsedStock;
    }
}
