using Microsoft.EntityFrameworkCore;
using Shiko.Community.Api.Models;

namespace Shiko.Community.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<CommunityLink> CommunityLinks => Set<CommunityLink>();
}