using MassTransit;
using NSubstitute;

namespace Amped.Core.Tests.UnitTests
{
    internal class TestConsumeContextBuilder<T> where T : class
    {
        private T _value;
        
        public TestConsumeContextBuilder<T> WithValue(T value)
        {
            _value = value;
            return this;
        }

        public ConsumeContext<T> Build()
        {
            var context = Substitute.For<ConsumeContext<T>>();
            context.Message.Returns(_value);
            return context;
        }
    }
}