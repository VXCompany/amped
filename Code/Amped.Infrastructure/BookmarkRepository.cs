namespace Amped.Core;

public class BookmarkRepository : IBookmarkRepository
{
    private readonly AmpedDbContext _db;

    public BookmarkRepository(AmpedDbContext db)
    {
        _db = db;
    }

    public async Task Add(Bookmark bookmark)
    {
        _db.Add(bookmark);
        await _db.SaveChangesAsync();
    }
}