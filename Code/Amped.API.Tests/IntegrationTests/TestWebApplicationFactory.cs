using System;
using System.Linq;
using Amped.Core.NewBookmark;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Amped.API.Tests.IntegrationTests;

public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
            
        builder.ConfigureServices(services =>
        {
            services
                .Where(d => d.ServiceType.Namespace.Contains("MassTransit", StringComparison.OrdinalIgnoreCase))
                .ToList()
                .ForEach(d => services.Remove(d));
                
            services.AddMassTransit(x =>
            {
                x.AddConsumer<CreateBookmarkCommandHandler>();
                
                x.UsingInMemory((context, cfg) =>
                {
                    cfg.TransportConcurrencyLimit = 100;
                    cfg.ConfigureEndpoints(context);
                });
            });
        });
    }
}