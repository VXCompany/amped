using Microsoft.AspNetCore.Mvc;
using Profile.Models;

namespace Profile;

public static class Configuration
{
    public static void UseMyEndpoints(this WebApplication app)
    {
        app.MapGet("/get-profile", () => "To do");
        app.MapPost("/create-profile", CreateProfile);
    }
    
    static async Task<IResult> CreateProfile([FromBody] CreateProfileRequest request, [FromServices] ProfileDbContext db)
    {
        var profile = new Profile.Models.Profile
        {
            Nickname = request.Nickname,
            Bio = request.Bio
        };
        db.Add(profile);
        await db.SaveChangesAsync();

        return Results.Ok();
    }
}