using FootballBuddy.Profiles.Domain.Aggregates;
using FootballBuddy.Shared.Domain.Users;

namespace FootballBuddy.Profiles.Domain.Repositories;

public interface IProfileRepository
{
    Task<bool> ExistsAsync(UserId userId);
    Task AddAsync(Profile profile);
    Task UpdateAsync(Profile profile);
    
}