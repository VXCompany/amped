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
        public void NewItemShouldHaveRatingSetBeNull()
        {
            var sut = new ContentItem();

            sut.Rating.Should().BeNull();
        }

        [Fact]
        public void WhenMultipleDifferentRatings_RatingShouldBeAverage()
        {
            var sut = new ContentItem();
            
            sut.AddRating(new Rating(2));
            sut.AddRating(new Rating(4));

            sut.Rating.Should().Be(new Rating(3));
        }
    }
}