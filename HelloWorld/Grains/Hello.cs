using IGrains;
using Orleans;
using Orleans.Concurrency;
using System;
using System.Threading.Tasks;

namespace Grains
{
    [StatelessWorker]
    public class Hello : Grain, IHello
    {
        public async Task<string> SayHello(string name)
        {
            Console.WriteLine($"{DateTime.Now}:收到 {name} 请求");
            return await Task.FromResult($"{DateTime.Now}:{name} Say Hello World");
        }
    }
}
