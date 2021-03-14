using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Hosting;
using System.Reflection;
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
                        .AddMemoryGrainStorageAsDefault()
                        .ConfigureApplicationParts(x => x.AddApplicationPart(Assembly.GetEntryAssembly()));
                    ;
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
