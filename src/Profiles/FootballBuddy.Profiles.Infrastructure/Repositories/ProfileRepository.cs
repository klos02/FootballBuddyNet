using FootballBuddy.Profiles.Domain.Aggregates;
using FootballBuddy.Profiles.Domain.Repositories;
using FootballBuddy.Profiles.Infrastructure.Persistence;
using FootballBuddy.Shared.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace FootballBuddy.Profiles.Infrastructure.Repositories;

public class ProfileRepository : IProfileRepository
{
    private readonly ProfilesDbContext _profiles;

    public ProfileRepository(ProfilesDbContext profiles)
    {
        _profiles = profiles;
    }
    
    public async Task<bool> ExistsAsync(UserId userId, CancellationToken cancellationToken = default)
    {
        return  await _profiles.Profiles.AnyAsync(x => x.Id == userId, cancellationToken);
    }

    public async Task AddAsync(Profile profile, CancellationToken cancellationToken = default)
    {
        await _profiles.AddAsync(profile, cancellationToken);
    }
}