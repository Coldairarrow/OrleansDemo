using Orleans;
using System.Threading.Tasks;

namespace HelloWorld.IGrains
{
    internal interface IHello : IGrainWithIntegerKey
    {
        Task Say(string name);
    }
}
