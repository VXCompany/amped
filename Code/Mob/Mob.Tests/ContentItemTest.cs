using System;
using FluentAssertions;
using Xunit;

namespace Mob.Tests
{
    public class ContentItemTest
    {
        [Fact]
        public void ShouldHaveId()
        {
            var sut = new ContentItem();

            sut.Id.Should().NotBeEmpty();
        }

        [Fact]
        public void NewItemShouldHaveRatingSetTo0()
        {
            var sut = new ContentItem();

            sut.Rating.Should().Be(0);
        }
        
        
    }
}