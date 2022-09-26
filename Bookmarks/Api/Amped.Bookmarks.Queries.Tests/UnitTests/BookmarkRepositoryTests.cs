using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Amped.Bookmarks.Queries.Tests.UnitTests;

public class BookmarkRepositoryTests
{
    [Fact]
    public async Task Returns_Bookmarks()
    {
        const string dbName = "test";
        
        await using (var context = AmpedDbContextFactory.CreateContext(dbName))
        {
            var bookmark = Core.Bookmark.CreateUnreadBookmark(new Uri("https://totallyamped.com/why-amped-rulez"), "00000000-0000-0000-0000-000000000001");

            context.Bookmarks.Add(bookmark);

            await context.SaveChangesAsync();
        }

        await using (var context = AmpedDbContextFactory.CreateContext(dbName))
        {
            var sut = new BookmarkRepository(context);
                
            var bookmarks = await sut.GetAll();
        
            bookmarks.Should().NotBeEmpty();
        }
    }
}