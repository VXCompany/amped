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
    }

    public class BookmarkBuilder
    {
        private Uri _uri = new ("http://someuri.com");
        private bool _read;
        private string _owner = "parker";

        public BookmarkBuilder WithUri(Uri uri)
        {
            _uri = uri;
            return this;
        }

        public BookmarkBuilder WithRead(bool read)
        {
            _read = read;
            return this;
        }
        
        public BookmarkBuilder WithOwner(string owner)
        {
            _owner = owner;
            return this;
        }
        
        public Bookmark Build() => new (_uri, _owner, _read);
    }
}