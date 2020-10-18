using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Hosting;
using System.Threading.Tasks;

namespace HelloWorld
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                .UseOrleans(builder =>
                {
                    builder
                        .UseLocalhostClustering()
                        .AddMemoryGrainStorageAsDefault();
                })
                .ConfigureServices(services =>
                {
                    services.AddHostedService<Bootstrapper>();
                })
                .ConfigureLogging(builder =>
                {
                    builder.AddConsole(options =>
                    {
                        options.TimestampFormat = "HH:mm:ss.fff";
                    });
                })
                .RunConsoleAsync();
        }
    }
}
