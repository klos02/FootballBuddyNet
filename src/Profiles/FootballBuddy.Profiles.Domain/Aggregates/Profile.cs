using BuildingBlocks.Domain.Abstractions;
using FootballBuddy.Profiles.Domain.Enums;
using FootballBuddy.Shared.Domain.Users;

namespace FootballBuddy.Profiles.Domain.Aggregates;

public sealed class Profile : AggregateRoot
{
    public UserId Id { get; private set; }
    public string DisplayName { get; private set; }
    public Position PrefferedPosition { get; private set; }
    public IEnumerable<Position> SecondaryPositions { get; private set; }
}