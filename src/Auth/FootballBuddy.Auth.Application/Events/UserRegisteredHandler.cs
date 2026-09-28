using FootballBuddy.Auth.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FootballBuddy.Auth.Application.Events;

public sealed class UserRegisteredHandler
    : INotificationHandler<DomainEventNotification<UserRegisteredDomainEvent>>
{
    private readonly ILogger<UserRegisteredHandler> _logger;

    public UserRegisteredHandler(ILogger<UserRegisteredHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(
        DomainEventNotification<UserRegisteredDomainEvent> notification,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Registered user {UserId}",
            notification.Event.UserId);

        return Task.CompletedTask;
    }
}
