using System.Threading.Tasks;

namespace Amped.Core
{
    public interface IEventStream
    {
        Task Broadcast<T>(T message) where T : class;
    }
}