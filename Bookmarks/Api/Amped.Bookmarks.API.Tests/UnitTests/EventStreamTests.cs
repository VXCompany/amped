using System.Threading.Tasks;
using MassTransit;
using NSubstitute;
using Xunit;

namespace Amped.Bookmarks.API.Tests.UnitTests;

public class EventStreamTests
{
    [Fact]
    public async Task Can_Send_Messages_To_The_Queue()
    {
        var expected = new TestMessage { Foo = "Bar" };
        var bus = Substitute.For<IBus>();

        var sut = new EventStream(bus);
        await sut.Broadcast(expected);

        await bus.Received(1).Publish(Arg.Is(expected));
    }

    public class TestMessage
    {
        public string Foo { get; set; } = "bar";
    }
}