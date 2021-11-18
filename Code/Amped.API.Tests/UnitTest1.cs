using System;
using FluentAssertions;
using Xunit;

namespace Amped.API.Tests
{
    public class UnitTest1
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
            Uri expected = new Uri("http://someuri.com");
            
            var bookmark = new Bookmark(expected);

            bookmark.Uri.Should().Be(expected);
        }

        [Fact]
        public void Should_Set_Read()
        {
            Uri uri = new Uri("http://someuri.com");
            var read = true;

            var bookmark = new Bookmark(uri, read);

            bookmark.Read.Should().Be(true);
        }
    }

    public class Bookmark
    {
        public Uri Uri { get; set; }
        public object Read { get; internal set; }

        public Bookmark(Uri uri, bool read = false)
        {
            Uri = uri ?? throw new ArgumentNullException();
        }
    }
}