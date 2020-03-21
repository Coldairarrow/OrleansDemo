using Orleans;
using System.Threading.Tasks;

namespace IGrains
{
    public interface IHello : IGrainWithIntegerKey
    {
        Task<string> SayHello(string name);
    }
}
