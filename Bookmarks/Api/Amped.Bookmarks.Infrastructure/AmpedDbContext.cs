using Amped.Bookmarks.Core;
using Microsoft.EntityFrameworkCore;

namespace Amped.Bookmarks.Infrastructure;

public class AmpedDbContext : DbContext
{
    public AmpedDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Bookmark> Bookmarks { get; set; }
}