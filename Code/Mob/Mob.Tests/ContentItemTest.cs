using System;
using FluentAssertions;
using Xunit;
using Mob;

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
        
        [Fact]
        public void RatingIsNotHigherThenFive()
        {
            var sut = new ContentItem();
            
            Action action = ()=> sut.SetRating(6);
            action.Should().Throw<ArgumentException>();
            
        }

        [Fact]
        public void RatingIsNotLowerThanOne()
        {
            var sut = new ContentItem();

            Action action = () => sut.SetRating(0);
            action.Should().Throw<ArgumentException>();

        }
    }
}