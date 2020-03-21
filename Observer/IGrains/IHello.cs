using Orleans;
using System.Threading.Tasks;

namespace IGrains
{
    public interface IHello : IGrainWithIntegerKey
    {
        Task Subscribe(IChat observer);
        Task UnSubscribe(IChat observer);
        Task SendUpdateMessage(string message);
    }
}
