using IGrains;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SiloHost1
{
    class Bootstrapper : BackgroundService
    {
        private IGrainFactory _grainFactory;
        private Timer _timer;
        private readonly ILogger _logger;
        public Bootstrapper(IGrainFactory grainFactory, ILogger<Bootstrapper> logger)
        {
            _grainFactory = grainFactory;
            _logger = logger;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(async _ =>
            {
                try
                {
                    await _grainFactory.GetGrain<IHello>(0).SayHello("111");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
            }, null, 0, 1000);

            return Task.CompletedTask;
        }
    }
}
