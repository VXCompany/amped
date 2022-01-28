using System.Threading.Tasks;

namespace Amped.API
{
    public interface IBus
    {
        Task Send<T>(T message) where T : class;
        Task Publish<T>(T message) where T : class;
    }
}