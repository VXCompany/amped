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
            Uri? invalidUri = null;
            
            Action bookmark = () => new BookmarkBuilder()
                .WithUri(invalidUri)
                .Build();

            bookmark.Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public void Should_Set_Uri()
        {
            var expected = new Uri("http://someuri.com");
            
            var bookmark = new BookmarkBuilder()
                .WithUri(expected)
                .Build();
            
            bookmark.Uri.Should().Be(expected);
        }

        [Fact]
        public void Should_Set_Read()
        {
            const bool read = true;

            var bookmark = new BookmarkBuilder()
                .WithRead(read)
                .Build();
            
            bookmark.Read.Should().Be(true);
        }

        [Fact]
        public void Should_Specify_Whos_Bookmark_It_Is()
        {
            const string parker = "parker";

            var bookmark = new BookmarkBuilder()
                .WithOwner(parker)
                .Build();

            bookmark.Owner.Should().Be(parker);
        }

        [Fact]
        public void Should_Be_Marked_Read()
        {
            var bookmark = new BookmarkBuilder()
                .WithRead(false)
                .Build();

            bookmark.MarkRead();

            bookmark.Read.Should().BeTrue();
        }
    }
}