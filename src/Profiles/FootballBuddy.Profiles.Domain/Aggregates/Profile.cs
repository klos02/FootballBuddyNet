using BuildingBlocks.Domain.Abstractions;
using BuildingBlocks.Domain.Exceptions;
using FootballBuddy.Profiles.Domain.Enums;
using FootballBuddy.Shared.Domain.Users;

namespace FootballBuddy.Profiles.Domain.Aggregates;

public sealed class Profile : AggregateRoot
{
    public UserId Id { get; private set; }
    public string DisplayName { get; private set; }
    public Position? PreferredPosition { get; private set; }
    public IReadOnlyCollection<Position> SecondaryPositions => _secondaryPositions.AsReadOnly();
    
    
    
    private readonly List<Position> _secondaryPositions = new();
    
    
    private Profile() {}

    private Profile(UserId id, string displayName)
    {
        Id = id;
        DisplayName = displayName;
        
    }

    public static Profile Create(UserId id, string displayName)
    {
        if (string.IsNullOrWhiteSpace(displayName)) throw new DomainException("No display name provided");
        
        return new Profile(id, displayName);
    }
    
}