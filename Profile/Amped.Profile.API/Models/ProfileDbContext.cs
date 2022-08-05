using Microsoft.EntityFrameworkCore;

namespace Profile.Models;

public class ProfileDbContext : DbContext
{
    public ProfileDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Profile> Profiles { get; set; }
}