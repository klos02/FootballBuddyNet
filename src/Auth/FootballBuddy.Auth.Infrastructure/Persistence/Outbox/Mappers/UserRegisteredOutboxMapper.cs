using System.Text.Json;
using BuildingBlocks.Domain.Abstractions;
using FootballBuddy.Auth.Contracts.IntegrationEvents;
using FootballBuddy.Auth.Domain.Events;

namespace FootballBuddy.Auth.Infrastructure.Persistence.Outbox.Mappers;

public sealed class UserRegisteredOutboxMapper : IOutboxEventMapper
{
    public Type EventType => typeof(UserRegisteredDomainEvent);

    public OutboxMessage Map(IDomainEvent domainEvent)
    {
        if (domainEvent is not UserRegisteredDomainEvent registered)
        {
            throw new ArgumentException(
                "Expected a user registered domain event.", nameof(domainEvent));
        }

        var integrationEvent = new UserRegisteredV1(
            registered.EventId,
            registered.UserId.Value,
            registered.OccurredOn);

        return new OutboxMessage(
            integrationEvent.EventId,
            "auth.user-registered.v1",
            JsonSerializer.Serialize(integrationEvent),
            integrationEvent.OccurredOn);
    }
}
