using System;
using System.Linq;
using Amped.Bookmarks.Core.NewBookmark;
using Amped.Bookmarks.Infrastructure;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Amped.Bookmarks.API.Tests.IntegrationTests;

public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
            
        builder.ConfigureServices(services =>
        {
            services
                .Where(d => d.ServiceType.Namespace != null && d.ServiceType.Namespace.Contains("MassTransit", StringComparison.OrdinalIgnoreCase))
                .ToList()
                .ForEach(d => services.Remove(d));
                
            services.AddMassTransit(x =>
            {
                x.AddConsumer<CreateBookmarkCommandHandler>();
                
                x.UsingInMemory((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                });
            });

            var dbContextDescriptor = services.First(d => d.ServiceType == typeof(AmpedDbContext));
            services.Remove(dbContextDescriptor);

            services.AddSqlite<AmpedDbContext>(":memory:");

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AmpedDbContext>();
        });
    }
}