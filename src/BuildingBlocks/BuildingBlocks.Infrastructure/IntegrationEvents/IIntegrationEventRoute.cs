namespace BuildingBlocks.Infrastructure.IntegrationEvents;

public interface IIntegrationEventRoute
{
    string EventType { get; }

    Task DispatchAsync(
        string payload,
        CancellationToken cancellationToken = default);

}