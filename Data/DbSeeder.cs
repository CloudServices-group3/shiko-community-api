using Microsoft.EntityFrameworkCore;
using Shiko.Community.Api.Models;

namespace Shiko.Community.Api.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (await db.CommunityLinks.AnyAsync())
        {
            return;
        }

        var links = new List<CommunityLink>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Slack Community",
                Url = "https://slack.com",
                IconName = "slack",
                SortOrder = 1,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Discord Helpline",
                Url = "https://discord.com",
                IconName = "discord",
                SortOrder = 2,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            }
        };

        db.CommunityLinks.AddRange(links);
        await db.SaveChangesAsync();
    }
}