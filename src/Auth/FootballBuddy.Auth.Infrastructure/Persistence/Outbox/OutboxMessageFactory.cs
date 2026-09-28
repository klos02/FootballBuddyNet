using BuildingBlocks.Domain.Abstractions;

namespace FootballBuddy.Auth.Infrastructure.Persistence.Outbox;

public sealed class OutboxMessageFactory
{
    private readonly IReadOnlyDictionary<Type, IOutboxEventMapper> _mappers;

    public OutboxMessageFactory(IEnumerable<IOutboxEventMapper> mappers)
    {
        _mappers = mappers.ToDictionary(mapper => mapper.EventType);
    }

    public OutboxMessage Create(IDomainEvent domainEvent)
    {
        if (!_mappers.TryGetValue(domainEvent.GetType(), out var mapper))
        {
            throw new InvalidOperationException(
                $"No outbox mapper registered for domain event {domainEvent.GetType().Name}.");
        }

        return mapper.Map(domainEvent);
    }
}
