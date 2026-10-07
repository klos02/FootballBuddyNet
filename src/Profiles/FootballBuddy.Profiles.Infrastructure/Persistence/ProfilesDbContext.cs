using FootballBuddy.Profiles.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace FootballBuddy.Profiles.Infrastructure.Persistence;

public sealed class ProfilesDbContext : DbContext
{
    public ProfilesDbContext(DbContextOptions<ProfilesDbContext> options) : base(options)
    {
        
    }

    public DbSet<Profile> Profiles => Set<Profile>();
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProfilesDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}