using IGrains;
using Orleans;
using System.Threading.Tasks;

namespace Grains
{
    public class Hello : Grain, IHello
    {
        private ObserverSubscriptionManager<IChat> _subsManager;

        public override async Task OnActivateAsync()
        {
            // We created the utility at activation time.
            _subsManager = new ObserverSubscriptionManager<IChat>();
            await base.OnActivateAsync();
        }

        // Clients call this to subscribe.
        public Task Subscribe(IChat observer)
        {
            if (!_subsManager.IsSubscribed(observer))
            {
                _subsManager.Subscribe(observer);
            }
            return Task.CompletedTask;
        }

        //Also clients use this to unsubscribe themselves to no longer receive the messages.
        public Task UnSubscribe(IChat observer)
        {
            if (_subsManager.IsSubscribed(observer))
            {
                _subsManager.Unsubscribe(observer);
            }
            return Task.CompletedTask;
        }

        public Task SendUpdateMessage(string message)
        {
            _subsManager.Notify(s => s.ReceiveMessage(message));
            return Task.CompletedTask;
        }
    }
}
