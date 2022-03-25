using Amped.Core;
using Amped.Core.NewBookmark;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;

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

        services.AddSqlite<AmpedDbContext>("Data Source=amped.db");

        services.AddControllers();
        services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Amped.API", Version = "v1"}); });

        ConfigureMassTransit(services);

        services.AddSignalRCore();
        
    }

    private void ConfigureMassTransit(IServiceCollection services)
    {
        var rabbitMqHost = Configuration.GetValue<string>("RABBITMQ_HOST");
        var rabbitMqPort = Configuration.GetValue<ushort>("RABBITMQ_PORT");
        var rabbitMqUser = Configuration.GetValue<string>("RABBITMQ_USER");
        var rabbitMqPassword = Configuration.GetValue<string>("RABBITMQ_PASSWORD");

        services.AddMassTransit(x =>
        {
            x.AddConsumer<CreateBookmarkCommandHandler>();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitMqHost, rabbitMqPort, "/", h =>
                {
                    h.Username(rabbitMqUser);
                    h.Password(rabbitMqPassword);
                });

                cfg.ConfigureEndpoints(context);
            });
        });
        services.AddMassTransitHostedService();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AmpedDbContext db)
    {
        // db.Database.EnsureCreated();
        db.Database.Migrate();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Amped.API v1"));
            app.UseCors(b => b
                .AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod());
        }

        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}