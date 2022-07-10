using System;
using System.Data.Common;
using Amped.Core;
using Amped.Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Amped.Queries.Tests.UnitTests;

public class AmpedDbContextFactory : IDisposable
{
    private DbConnection? _connection;
    
    private DbContextOptions<AmpedDbContext> CreateOptions()
    {
        return new DbContextOptionsBuilder<AmpedDbContext>()
            .UseSqlite(_connection)
            .Options;
    }

    public AmpedDbContext CreateContext()
    {
        if (_connection == null)
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = CreateOptions();
            using (var context = new AmpedDbContext(options))
            {
                context.Database.EnsureCreated();
            }
        }

        return new AmpedDbContext(CreateOptions());
    }

    public void Dispose()
    {
        if (_connection != null)
        {
            _connection.Dispose();
            _connection = null;
        }
    }
}