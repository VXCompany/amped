using Amped.Bookmarks.Core;
using Microsoft.EntityFrameworkCore;

namespace Amped.Bookmarks.Infrastructure;

public class AmpedDbContext : DbContext
{
    public AmpedDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Bookmark> Bookmarks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bookmark>(b =>
        {
            b.ToContainer("Bookmarks");
            b.Property(b => b.Id).ToJsonProperty("id");
            b.HasPartitionKey(b => b.Owner);
            b.HasKey(b => new {b.Owner, b.Id});
        });
    }
}