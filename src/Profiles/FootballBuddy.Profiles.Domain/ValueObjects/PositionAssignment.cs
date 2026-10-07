using BuildingBlocks.Domain.Exceptions;
using FootballBuddy.Profiles.Domain.Enums;

namespace FootballBuddy.Profiles.Domain.ValueObjects;

public sealed record PositionAssignment
{
    public Position Position { get; }

    public PositionAssignment(Position position)
    {
        if (!Enum.IsDefined(position))
        {
            throw new DomainException("Invalid position");
        }

        Position = position;
    }
}
