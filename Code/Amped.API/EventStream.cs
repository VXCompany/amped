using System.Threading.Tasks;
using Amped.Core;
using MassTransit;
namespace Amped.API;

public class EventStream : IEventStream
{
    private readonly IBus _bus;

    public EventStream(IBus bus) => _bus = bus;
        
    public async Task Broadcast<T>(T message) where T : class => await _bus.Publish(message);
}