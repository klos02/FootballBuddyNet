using BuildingBlocks.Domain.Abstractions;
using MediatR;

namespace FootballBuddy.Auth.Application.Events;

public sealed record DomainEventNotification<TEvent>(TEvent Event) : INotification
    where TEvent : IDomainEvent;
