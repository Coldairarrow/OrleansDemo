using EFCore.Sharding;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using StockAccess.DependencyInjection;
using System;

namespace StockAccess
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CustomContext>
    {
        static DesignTimeDbContextFactory()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddDatabase();
            ServiceProvider = services.BuildServiceProvider();
            new EFCoreShardingBootstrapper(ServiceProvider).StartAsync(default).Wait();
        }

        public static readonly IServiceProvider ServiceProvider;

        /// <summary>
        /// 创建数据库上下文
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public CustomContext CreateDbContext(string[] args)
        {
            var db = ServiceProvider
                .GetService<IDbFactory>()
                .GetDbContext(new DbContextParamters
                {
                    ConnectionString = DatabaseExtentions.ConString,
                    DbType = DatabaseType.SqlServer,
                });

            return new CustomContext(db);
        }
    }
}
