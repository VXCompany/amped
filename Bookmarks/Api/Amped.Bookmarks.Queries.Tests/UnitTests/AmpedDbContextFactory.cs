using System;
using Amped.Bookmarks.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Amped.Bookmarks.Queries.Tests.UnitTests;

public static class AmpedDbContextFactory
{
    private static DbContextOptions<AmpedDbContext> CreateOptions(string dbName) =>
        new DbContextOptionsBuilder<AmpedDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

    public static AmpedDbContext CreateContext(string dbName) => new (CreateOptions(dbName));

}