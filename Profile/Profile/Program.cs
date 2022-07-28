/*
 * TODO
 *
 * Profile:
 *  Bio
 *  Nickname
 *  Avatar (self-hosted, picture)
 *
 * POST (done)
 * GET
 * PUT
 * 
 */

using Microsoft.EntityFrameworkCore;
using Profile;
using Profile.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProfileDbContext>(config =>
{
    config.UseSqlite("Data Source=profile.db");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMyEndpoints();

app.Run();
