using HelloWorld.IGrains;
using Microsoft.Extensions.Logging;
using Orleans;
using System.Threading.Tasks;

namespace HelloWorld.Grains
{
    internal class HelloGrain : Grain, IHello
    {
        private readonly ILogger _logger;
        public HelloGrain(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(GetType());
        }
        public Task Say(string name)
        {
            _logger.LogInformation("{Name} Say Hello", name);

            return Task.CompletedTask;
        }
    }
}
