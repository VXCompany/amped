using amped_bookmark;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// add Sqlite
builder.Services.AddDbContext<BookmarkContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BookmarkContext")));

// add Auth0 authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["Auth0:Authority"];
    options.Audience = builder.Configuration["Auth0:Audience"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        NameClaimType = ClaimTypes.NameIdentifier
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("write:bookmark", policy =>
    {
        var issuer = $"{builder.Configuration["Auth0:Authority"]}/";
        policy.Requirements.Add(new
            HasScopeRequirement("write:bookmark", issuer));
    });
});

builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(
                builder.Configuration["CORS:ClientOriginUrl"] ?? string.Empty)
            .WithHeaders(new string[] {
                HeaderNames.ContentType,
                HeaderNames.Authorization,
            })
            .WithMethods("GET","POST")
            .SetPreflightMaxAge(TimeSpan.FromSeconds(86400));
    });
});

var app = builder.Build();

app.Urls.Add($"http://+:{builder.Configuration["Port"]}");

// add Auth0 authentication
app.UseAuthentication();
app.UseAuthorization();

// add CORS
app.UseCors();

var bookmarks = app.MapGroup("/bookmark");

// get a bookmark by id
bookmarks.MapGet("/", async (BookmarkContext db) =>
{
    var bookmarks = await db.Bookmarks.ToListAsync();
    return bookmarks;
});

// post a bookmark
bookmarks.MapPost("/", async (CreateBookmarkRequest request, ClaimsPrincipal user, BookmarkContext db) =>
{
    // map bookmark to domain object
    var bookmark = new Bookmark
    {
        Url = request.Uri,
        ProfileId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value,
        Read = false
    };
    
    // use Sqlite to store the bookmark
    db.Add(bookmark);
    await db.SaveChangesAsync();

    return bookmark;
}).RequireAuthorization("write:bookmark");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<BookmarkContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.Run();

