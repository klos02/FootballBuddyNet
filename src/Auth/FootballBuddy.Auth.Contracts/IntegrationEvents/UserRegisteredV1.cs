namespace FootballBuddy.Auth.Contracts.IntegrationEvents;

public sealed record UserRegisteredV1(Guid EventId, Guid UserId, DateTime OccurredOn);