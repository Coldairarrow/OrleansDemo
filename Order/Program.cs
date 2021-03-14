using Colder.MessageBus.Abstractions;
using Colder.MessageBus.MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Hosting;
using StockAccess.DependencyInjection;
using System.Reflection;

namespace StockAccess
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseOrleans(builder =>
                {
                    builder
                        .UseLocalhostClustering()
                        .AddMemoryGrainStorageAsDefault()
                        .ConfigureApplicationParts(x => x.AddApplicationPart(Assembly.GetEntryAssembly()))
                        ;
                })
                .ConfigureServices(services =>
                {
                    services.AddDatabase();
                    services.AddHostedService<AppBootstrapper>();
                    services.AddMessageBus(new MessageBusOptions
                    {
                        Transport = TransportType.InMemory
                    }, Assembly.GetEntryAssembly().GetName().Name);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
