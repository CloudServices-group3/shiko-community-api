using Microsoft.EntityFrameworkCore;
using Shiko.Community.Api.Data;
using Shiko.Community.Api.Models;

namespace Shiko.Community.Api.Endpoints;

public static class CommunityEndpoints
{
    public static void MapCommunityEndpoints(this WebApplication app)
    {
        app.MapGet("/api/community-links", async (AppDbContext db) =>
        {
            var links = await db.CommunityLinks
                .Where(link => link.IsActive)
                .OrderBy(link => link.SortOrder)
                .ToListAsync();

            return Results.Ok(links);
        });

        app.MapGet("/api/community-links/{id:guid}", async (Guid id, AppDbContext db) =>
        {
            var link = await db.CommunityLinks.FindAsync(id);

            return link is null
                ? Results.NotFound()
                : Results.Ok(link);
        });

        app.MapPost("/api/community-links", async (CommunityLink link, AppDbContext db) =>
        {
            link.Id = Guid.NewGuid();
            link.CreatedAt = DateTime.UtcNow;

            db.CommunityLinks.Add(link);
            await db.SaveChangesAsync();

            return Results.Created($"/api/community-links/{link.Id}", link);
        });

        app.MapPut("/api/community-links/{id:guid}", async (Guid id, CommunityLink updatedLink, AppDbContext db) =>
        {
            var link = await db.CommunityLinks.FindAsync(id);

            if (link is null)
            {
                return Results.NotFound();
            }

            link.Title = updatedLink.Title;
            link.Url = updatedLink.Url;
            link.IconName = updatedLink.IconName;
            link.SortOrder = updatedLink.SortOrder;
            link.IsActive = updatedLink.IsActive;
            link.UpdatedAt = DateTime.UtcNow;

            await db.SaveChangesAsync();

            return Results.Ok(link);
        });

        app.MapDelete("/api/community-links/{id:guid}", async (Guid id, AppDbContext db) =>
        {
            var link = await db.CommunityLinks.FindAsync(id);

            if (link is null)
            {
                return Results.NotFound();
            }

            db.CommunityLinks.Remove(link);
            await db.SaveChangesAsync();

            return Results.NoContent();
        });
    }
}