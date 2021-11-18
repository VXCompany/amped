using System;
using FluentAssertions;
using Xunit;

namespace Amped.API.Tests
{
    public class BookmarkTests
    {
        private const string DummyOwner = "dummy owner";

        [Fact]
        public void Should_Throw_ArgumentNullException_When_No_Uri()
        {
            Uri invalidUri = null;
            
            Action bookmark = () => new Bookmark(invalidUri, DummyOwner);

            bookmark.Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void Should_Set_Uri()
        {
            var expected = new Uri("http://someuri.com");
            
            var bookmark = new Bookmark(expected, DummyOwner);

            bookmark.Uri.Should().Be(expected);
        }

        [Fact]
        public void Should_Set_Read()
        {
            var uri = new Uri("http://someuri.com");
            var read = true;

            var bookmark = new Bookmark(uri, DummyOwner, read);

            bookmark.Read.Should().Be(true);
        }

        [Fact]
        public void Should_Specify_Whos_Bookmark_It_Is()
        {
            var uri = new Uri("http://someuri.com");
            var read = true;
            var parker = "parker";

            var bookmark = new Bookmark(uri, parker, read);

            bookmark.Owner.Should().Be(parker);
        }

    }
}