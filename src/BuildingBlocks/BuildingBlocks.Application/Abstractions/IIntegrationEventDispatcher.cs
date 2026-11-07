namespace BuildingBlocks.Application.Abstractions;

public interface IIntegrationEventDispatcher
{
    Task DispatchAsync<TEvent>(TEvent integrationEvent,
        CancellationToken cancellationToken = default) where TEvent : class;
}