using BuildingBlocks.Domain.Abstractions;
using FootballBuddy.Auth.Application.Abstractions;
using FootballBuddy.Auth.Infrastructure.Persistence.Outbox;

namespace FootballBuddy.Auth.Infrastructure.Persistence;

public class AuthUnitOfWork : IAuthUnitOfWork
{
    
    private readonly AuthDbContext _dbContext;
    private readonly OutboxMessageFactory _outboxMessageFactory;
    
    public AuthUnitOfWork(AuthDbContext dbContext, OutboxMessageFactory outboxMessageFactory)
    {
        _dbContext = dbContext;
        _outboxMessageFactory = outboxMessageFactory;
    }
    
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var aggregates = _dbContext.ChangeTracker
            .Entries<AggregateRoot>()
            .Select(entry => entry.Entity)
            .Where(aggregate => aggregate.DomainEvents.Count > 0)
            .ToArray();

        var messages = aggregates
            .SelectMany(aggregate => aggregate.DomainEvents)
            .Select(_outboxMessageFactory.Create)
            .ToArray();

        var trackedMessageIds = _dbContext.OutboxMessages.Local
            .Select(message => message.Id)
            .ToHashSet();

        _dbContext.OutboxMessages.AddRange(
            messages.Where(message => trackedMessageIds.Add(message.Id)));

        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        foreach (var aggregate in aggregates)
        {
            aggregate.ClearDomainEvents();
        }

        return result;
    }
}
