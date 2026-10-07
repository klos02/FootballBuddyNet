namespace BuildingBlocks.Application.Abstractions;

public interface IIntegrationEventHandler<TEvent> where TEvent : class
{
    Task HandleAsync(TEvent integrationEvent, CancellationToken cancellationToken = default);
}