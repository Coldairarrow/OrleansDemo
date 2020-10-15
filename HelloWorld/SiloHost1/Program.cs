using Grains;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SiloHost1
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var invariant = "System.Data.SqlClient";
            var connectionString = "Data Source=192.168.56.1;Initial Catalog=Orleans;User Id=sa;Password=123456;";
            string ip = string.Empty;
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                ip = Environment.GetEnvironmentVariable("IP", EnvironmentVariableTarget.Machine);
            }
            else
            {
                ip = Environment.GetEnvironmentVariable("IP", EnvironmentVariableTarget.Process);
            }

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
                            options.ProbeTimeout = TimeSpan.FromSeconds(3);
                            options.IAmAliveTablePublishTimeout = TimeSpan.FromSeconds(3);
                            options.NumVotesForDeathDeclaration = 1;
                        })
                        .Configure<EndpointOptions>(options =>
                        {
                            options.AdvertisedIPAddress = IPAddress.Parse(ip);
                            //options.SiloPort = IpHelper.GetFirstAvailablePort();
                        })
                        .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(Hello).Assembly).WithReferences())
                        .AddMemoryGrainStorageAsDefault()
                        .AddAdoNetGrainStorageAsDefault(options =>
                        {
                            options.ConnectionString = connectionString;
                            options.Invariant = invariant;
                        })
                        ;
                })
                .ConfigureServices(services =>
                {
                    services.AddHostedService<Bootstrapper>();
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
