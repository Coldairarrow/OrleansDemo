using Grains;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using System.Net;
using System.Threading.Tasks;

namespace SiloHost2
{
    class Program
    {
        public static Task Main(string[] args)
        {
            int siloPort = 11112;
            int gatewayPort = 30002;
            return new HostBuilder()
                .UseOrleans(builder =>
                {
                    builder
                        .UseLocalhostClustering(siloPort, gatewayPort)
                        .Configure<ClusterOptions>(options =>
                        {
                            options.ClusterId = "dev";
                            options.ServiceId = "dev";
                        })
                        .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                        .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(Hello).Assembly).WithReferences())
                        .AddMemoryGrainStorage(name: "ArchiveStorage");
                })
                .ConfigureServices(services =>
                {
                    services.Configure<ConsoleLifetimeOptions>(options =>
                    {
                        options.SuppressStatusMessages = true;
                    });
                })
                .ConfigureLogging(builder =>
                {
                    builder.AddConsole();
                })
                .RunConsoleAsync();
        }
    }
}
