using FootballBuddy.Auth.Application.Abstractions;

namespace FootballBuddy.Auth.Infrastructure.Persistence;

public class AuthUnitOfWork : IAuthUnitOfWork
{
    
    private readonly AuthDbContext _dbContext;
    
    public AuthUnitOfWork(AuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}