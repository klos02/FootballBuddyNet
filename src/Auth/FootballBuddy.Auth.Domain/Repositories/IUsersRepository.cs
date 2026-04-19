using FootballBuddy.Auth.Domain.Aggregates;
using FootballBuddy.Shared.Domain.Users;

namespace FootballBuddy.Auth.Domain.Repositories;

public interface IUsersRepository
{
    public Task<User> GetUserAsync(UserId userId, CancellationToken cancellationToken = default);
    public Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
    public Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
    public Task AddAsync(User user, CancellationToken cancellationToken = default);
    public Task UpdateAsync(User user, CancellationToken cancellationToken = default);
    public Task DeleteAsync(User user, CancellationToken cancellationToken = default);
}