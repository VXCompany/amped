using System.Threading.Tasks;
using MassTransit;
using NSubstitute;
using Xunit;

namespace Amped.Bookmarks.API.Tests.UnitTests;

public class CommandQueueTests
{
    [Fact]
    public async Task Can_Send_Command()
    {
        var expected = new TestMessage { Foo = "Bar" };
        var bus = Substitute.For<IBus>();

        var sut = new CommandQueue(bus);
        await sut.Send(expected);

        await bus.Received(1).Publish(Arg.Is(expected));
    }

    public class TestMessage
    {
        public string Foo { get; set; } = "bar";
    }
}