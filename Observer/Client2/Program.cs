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

            var friend = client.GetGrain<IHello>(0);

            while (true)
            {
                try
                {
                    Thread.Sleep(1000);
                    await friend.SendUpdateMessage("推送消息");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
