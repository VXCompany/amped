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
        modelBuilder.Entity<Bookmark>(entity =>
        {
            entity.ToContainer("Bookmarks");
            entity.Property(b => b.Id).ToJsonProperty("id");
            entity.Property(b => b.Owner).ToJsonProperty("owner");
            entity.HasPartitionKey(b => b.Owner);
            
            entity.HasKey(b => new {b.Owner, b.Id});
        });
    }
}