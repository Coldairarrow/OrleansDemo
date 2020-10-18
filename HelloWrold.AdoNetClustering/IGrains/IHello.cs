using Orleans;
using System.Threading.Tasks;

namespace HelloWrold.AdoNetClustering.IGrains
{
    internal interface IHello : IGrainWithIntegerKey
    {
        Task Say(string name);
    }
}
