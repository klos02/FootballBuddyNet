using FootballBuddy.Profiles.Application.Abstractions;
using FootballBuddy.Profiles.Domain.Repositories;

namespace FootballBuddy.Profiles.Infrastructure.Persistence;

public class ProfileUnitOfWork : IProfileUnitOfWork
{
    private readonly ProfilesDbContext _profiles;
    
    public ProfileUnitOfWork(ProfilesDbContext profiles)
    {
        _profiles = profiles;
    }
    
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _profiles.SaveChangesAsync(cancellationToken);
    }
}