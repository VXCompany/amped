using System.Threading.Tasks;
using Amped.Core;
using MassTransit;

namespace Amped.API;

public class CommandQueue : ICommandQueue
{
    private readonly IBus _bus;

    public CommandQueue(IBus bus) => _bus = bus;
        
    public Task Send<T>(T message) where T : class => _bus.Publish(message);
}