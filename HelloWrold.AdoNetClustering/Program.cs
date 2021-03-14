using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace HelloWrold.AdoNetClustering
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            //初始化SQL:http://dotnet.github.io/orleans/Documentation/clusters_and_clients/configuration_guide/adonet_configuration.html
            var invariant = "Microsoft.Data.SqlClient";
            var connectionString = "Data Source=127.0.0.1;Initial Catalog=Orleans;User Id=sa;Password=123456;";

            await Host.CreateDefaultBuilder(args)
                .UseOrleans(builder =>
                {
                    builder
                        .UseAdoNetClustering(options =>
                        {
                            options.ConnectionString = connectionString;
                            options.Invariant = invariant;
                        })
                        .Configure<ClusterOptions>(options =>
                        {
                            options.ClusterId = "orleans.test";
                            options.ServiceId = "orleans.test";
                        })
                        .Configure<ClusterMembershipOptions>(options =>
                        {
                            //及时下线
                            options.ProbeTimeout = TimeSpan.FromSeconds(3);
                            options.IAmAliveTablePublishTimeout = TimeSpan.FromSeconds(3);
                            options.NumVotesForDeathDeclaration = 1;
                        })
                        .Configure<EndpointOptions>(options =>
                        {
                            options.AdvertisedIPAddress = IPAddress.Loopback;
                        })
                        .ConfigureApplicationParts(x => x.AddApplicationPart(Assembly.GetEntryAssembly()))
                        .AddAdoNetGrainStorageAsDefault(options =>
                        {
                            options.ConnectionString = connectionString;
                            options.Invariant = invariant;
                        });
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
