using System.Threading.Tasks;
using MassTransit;

namespace Amped.API
{
    public class Bus : IBus
    {
        private readonly MassTransit.IBus _bus;

        public Bus(MassTransit.IBus bus)
        {
            _bus = bus;
        }

        public Task Send<T>(T message) where T : class => _bus.Send(message);
        
        public Task Publish<T>(T message) where T : class => _bus.Publish(message);
    }
}