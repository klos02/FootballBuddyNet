using BuildingBlocks.Domain.Abstractions;
using FootballBuddy.Auth.Application.Abstractions;
using FootballBuddy.Auth.Application.Events;
using FootballBuddy.Auth.Domain.Events;
using MediatR;

namespace FootballBuddy.Auth.Infrastructure.Persistence;

public class AuthUnitOfWork : IAuthUnitOfWork
{
    
    private readonly AuthDbContext _dbContext;
    private readonly IPublisher _publisher;
    
    public AuthUnitOfWork(AuthDbContext dbContext, IPublisher publisher)
    {
        _dbContext = dbContext;
        _publisher = publisher;
    }
    
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var aggregates = _dbContext.ChangeTracker
            .Entries<AggregateRoot>()
            .Select(entry => entry.Entity)
            .Where(aggregate => aggregate.DomainEvents.Count > 0)
            .ToArray();

        var notifications = aggregates
            .SelectMany(aggregate => aggregate.DomainEvents)
            .Select(ToNotification)
            .ToArray();

        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        foreach (var aggregate in aggregates)
        {
            aggregate.ClearDomainEvents();
        }

        foreach (var notification in notifications)
        {
            await _publisher.Publish(notification, cancellationToken);
        }

        return result;
    }

    private static INotification ToNotification(IDomainEvent domainEvent) => domainEvent switch
    {
        UserRegisteredDomainEvent registered =>
            new DomainEventNotification<UserRegisteredDomainEvent>(registered),
        _ => throw new InvalidOperationException(
            $"Unsupported domain event: {domainEvent.GetType().Name}")
    };
}
