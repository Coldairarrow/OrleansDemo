using IGrains;
using Microsoft.Extensions.Logging;
using Orleans;
using System;
using System.Threading.Tasks;

namespace Grains
{
    public class Hello : Grain, IHello
    {
        private readonly ILogger _logger;
        public Hello(ILoggerFactory  loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(GetType());
        }
        public override Task OnActivateAsync()
        {
            _logger.LogInformation("激活**********************************");

            return base.OnActivateAsync();
        }
        public async Task<string> SayHello(string name)
        {
            Console.WriteLine($"{DateTime.Now}:收到 {name} 请求");
            return await Task.FromResult($"{DateTime.Now}:{name} Say Hello World");
        }
    }
}
