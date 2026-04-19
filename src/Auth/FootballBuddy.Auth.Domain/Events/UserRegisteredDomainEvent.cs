using BuildingBlocks.Domain.Events;
using FootballBuddy.Shared.Domain.Users;

namespace FootballBuddy.Auth.Domain.Events;

public sealed record UserRegisteredDomainEvent(UserId UserId) : DomainEvent;
