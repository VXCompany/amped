using Amped.Core;
using Amped.Core.NewBookmark;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Amped.API;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IBookmarkRepository, BookmarkRepository>();
        services.AddTransient<ICommandQueue, CommandQueue>();
        services.AddTransient<IEventStream, EventStream>();
        services.AddTransient<Queries.IBookmarkRepository, Queries.BookmarkRepository>(); // yuck...

        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Amped.API", Version = "v1" });
        });
        
        services.AddMassTransit(x =>
        {
            x.AddConsumer<CreateBookmarkCommandHandler>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("user");
                    h.Password("PASSWORD");
                });
                    
                cfg.ConfigureEndpoints(context);
            });
        });

        services.AddMassTransitHostedService();
        
        services.AddOptions<StartupOptions>()
            .Configure<IConfiguration>((options, config) =>
            {
                config.Bind(options);
            });
        
        services.AddSignalRCore();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Amped.API v1"));
        }

        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}

