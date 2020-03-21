using IGrains;
using Orleans;
using Orleans.Configuration;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var gateways = new IPEndPoint[]
            {
                new IPEndPoint(IPAddress.Loopback, 30000),
                new IPEndPoint(IPAddress.Loopback, 30002),
            };
            var client = new ClientBuilder()
                    .UseStaticClustering(gateways)
                    .Configure<ClusterOptions>(options =>
                    {
                        options.ClusterId = "dev";
                        options.ServiceId = "dev";
                    })
                    .Build();
            await client.Connect(async ex => await Task.FromResult(true));

            while (true)
            {
                try
                {
                    Thread.Sleep(1000);
                    var helloService = client.GetGrain<IHello>(0);
                    var res = await helloService.SayHello("小明");
                    Console.WriteLine(res);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
