using System;
using FluentAssertions;
using Xunit;

namespace Mob.Tests
{
    public class RecordTests
    {
        [Fact]
        public void RatingIsNotHigherThenFive()
        {
            Action action = () => new Rating(6);
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void RatingIsNotLowerThanOne()
        {
            Action action = () => new Rating(0);
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void RatingCanBeOne()
        {
            Action action = () => new Rating(1);
            action.Should().NotThrow<ArgumentException>();
        }
        
        [Fact]
        public void RatingCanBeFive()
        {
            Action action = () => new Rating(5);
            action.Should().NotThrow<ArgumentException>();
        }

    }
}