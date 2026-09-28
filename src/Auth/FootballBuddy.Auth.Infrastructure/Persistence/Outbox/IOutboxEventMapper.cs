using BuildingBlocks.Domain.Abstractions;

namespace FootballBuddy.Auth.Infrastructure.Persistence.Outbox;

public interface IOutboxEventMapper
{
    Type EventType { get; }
    OutboxMessage Map(IDomainEvent domainEvent);
}
