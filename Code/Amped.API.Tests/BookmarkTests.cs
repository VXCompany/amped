using System;
using FluentAssertions;
using Xunit;

namespace Amped.API.Tests
{
    public class BookmarkTests
    {
        [Fact]
        public void Should_Throw_ArgumentNullException_When_No_Uri()
        {
            Uri invalidUri = null;
            
            Action bookmark = () => new Bookmark(invalidUri);

            bookmark.Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void Should_Set_Uri()
        {
            var expected = new Uri("http://someuri.com");
            
            var bookmark = new Bookmark(expected);

            bookmark.Uri.Should().Be(expected);
        }

        [Fact]
        public void Should_Set_Read()
        {
            var uri = new Uri("http://someuri.com");
            var read = true;

            var bookmark = new Bookmark(uri, read);

            bookmark.Read.Should().Be(true);
        }

        [Fact]
        public void METHOD()
        {
            
        }

    }
}