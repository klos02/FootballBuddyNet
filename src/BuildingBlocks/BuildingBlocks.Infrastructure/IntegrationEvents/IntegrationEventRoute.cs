using System.Text.Json;
using BuildingBlocks.Application.Abstractions;

namespace BuildingBlocks.Infrastructure.IntegrationEvents;

public sealed class IntegrationEventRoute<TEvent> : IIntegrationEventRoute where TEvent : class
{
    
    private readonly IIntegrationEventDispatcher _dispatcher;
    public string EventType { get; }


    public IntegrationEventRoute(IIntegrationEventDispatcher dispatcher, string eventType)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(eventType);
        
        _dispatcher = dispatcher;
        EventType = eventType;
    }

    public Task DispatchAsync(string payload, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var integrationEvent = JsonSerializer.Deserialize<TEvent>(payload) ??
                               throw new InvalidOperationException($"Payload for {EventType} cannot be null.");
        
        return _dispatcher.DispatchAsync<TEvent>(integrationEvent, cancellationToken);
    }
}