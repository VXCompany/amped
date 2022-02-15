using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Amped.Queries.Tests.UnitTests
{
    public class BookmarkRepositoryTests
    {
        [Fact]
        public async Task Returns_Bookmarks()
        {
            var sut = new BookmarkRepository();

            IEnumerable<Bookmark> bookmarks = await sut.GetAll();

            bookmarks.Should().NotBeEmpty();
        }
    }
}