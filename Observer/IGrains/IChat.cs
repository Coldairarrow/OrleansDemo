using Orleans;

namespace IGrains
{
    public interface IChat : IGrainObserver
    {
        void ReceiveMessage(string message);
    }
}
