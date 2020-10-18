using EFCore.Sharding;
using Microsoft.Extensions.DependencyInjection;

namespace StockAccess.DependencyInjection
{
    public static class DatabaseExtentions
    {
        public static readonly string ConString = "Data Source=localhost;Initial Catalog=StockAccess;Integrated Security=True";
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddEFCoreSharding(options =>
            {
                options.UseDatabase(ConString, DatabaseType.SqlServer);

                options.MigrationsWithoutForeignKey();
            });

            return services;
        }
    }
}
