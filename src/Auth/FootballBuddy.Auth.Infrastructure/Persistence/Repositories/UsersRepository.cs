using FootballBuddy.Auth.Domain.Aggregates;
using FootballBuddy.Auth.Domain.Repositories;
using FootballBuddy.Shared.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace FootballBuddy.Auth.Infrastructure.Persistence.Repositories;

public class UsersRepository : IUsersRepository

{
    private readonly AuthDbContext _context;

    public UsersRepository(AuthDbContext context)
    {
        _context = context;
    }
    
    public async Task<User> GetUserAsync(UserId userId, CancellationToken cancellationToken = default)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }

    public async Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Users.AnyAsync(u => u.Email == email, cancellationToken);
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(user, cancellationToken);
    }
}