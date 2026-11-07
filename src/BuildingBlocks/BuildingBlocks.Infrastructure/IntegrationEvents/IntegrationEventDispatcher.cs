using BuildingBlocks.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Infrastructure.IntegrationEvents;

public sealed class IntegrationEventDispatcher : IIntegrationEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public IntegrationEventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }


    public async Task DispatchAsync<TEvent>(TEvent integrationEvent, CancellationToken cancellationToken = default)
        where TEvent : class
    {
        ArgumentNullException.ThrowIfNull(integrationEvent);
        cancellationToken.ThrowIfCancellationRequested();

        var handlers = _serviceProvider
            .GetServices<IIntegrationEventHandler<TEvent>>()
            .ToArray();

        if (handlers.Length == 0)
        {
            throw new InvalidOperationException(
                $"No handlers registered for {typeof(TEvent).Name}.");

        }

        foreach (var handler in handlers)
        {
            await handler.HandleAsync(integrationEvent, cancellationToken);
        }

    }

}