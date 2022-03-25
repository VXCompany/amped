using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Amped.Core;
using Microsoft.EntityFrameworkCore;

namespace Amped.Queries;

public interface IBookmarkRepository
{
    Task<IEnumerable<Bookmark>> GetAll();
}

public class BookmarkRepository : IBookmarkRepository
{
    private readonly AmpedDbContext _db;

    public BookmarkRepository(AmpedDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Bookmark>> GetAll()
    {
        return await _db.Bookmarks
            .Select(b => new Bookmark
            {
                Uri = b.Uri,
                Owner = Guid.Parse(b.Owner),
                Read = b.Read
            })
            .ToListAsync();
        
        // Todo: Implement some form of storage?
        // The following values are for Dimitri, now he can build some sort of a front-end :-)
        // var result = new[]
        // {
        //     new Bookmark
        //     {
        //         Id = 1, 
        //         Owner = new Guid("00000000-0000-0000-0000-000000000001"), 
        //         Read = false, 
        //         Uri = new Uri("https://totallyamped.com/why-amped-rulez")
        //     }
        // };
        //
        // return result;
    }
}