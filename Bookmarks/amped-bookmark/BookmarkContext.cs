namespace amped_bookmark;
using Microsoft.EntityFrameworkCore;

public  class BookmarkContext : DbContext
{
    public BookmarkContext(DbContextOptions<BookmarkContext> options)
        : base(options)
    {
    }

    public DbSet<Bookmark> Bookmarks { get; set; }
}