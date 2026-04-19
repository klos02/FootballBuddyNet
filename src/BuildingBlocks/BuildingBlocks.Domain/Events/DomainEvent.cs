using BuildingBlocks.Domain.Abstractions;

namespace BuildingBlocks.Domain.Events;

public class DomainEvent : IDomainEvent
{
    public Guid EventId { get; init; } = Guid.NewGuid();
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
}