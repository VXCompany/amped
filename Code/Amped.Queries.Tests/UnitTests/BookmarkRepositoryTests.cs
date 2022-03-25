using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Amped.Queries.Tests.UnitTests;

public class BookmarkRepositoryTests
{
    [Fact(Skip = "Determine best test strategy for infrastructure")]
    public async Task Returns_Bookmarks()
    {
        // var sut = new BookmarkRepository();
        //
        // IEnumerable<Bookmark> bookmarks = await sut.GetAll();
        //
        // bookmarks.Should().NotBeEmpty();
    }
}