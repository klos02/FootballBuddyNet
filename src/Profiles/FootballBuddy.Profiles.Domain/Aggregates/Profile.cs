using BuildingBlocks.Domain.Abstractions;
using BuildingBlocks.Domain.Exceptions;
using FootballBuddy.Profiles.Domain.Enums;
using FootballBuddy.Profiles.Domain.ValueObjects;
using FootballBuddy.Shared.Domain.Users;

namespace FootballBuddy.Profiles.Domain.Aggregates;

public sealed class Profile : AggregateRoot
{
    public UserId Id { get; private set; }
    public string? DisplayName { get; private set; }
    public Position? PreferredPosition { get; private set; }
    public IReadOnlyCollection<PositionAssignment> SecondaryPositions => _secondaryPositions.AsReadOnly();
    
    
    
    private readonly List<PositionAssignment> _secondaryPositions = new();
    
    
    private Profile() {}

    private Profile(UserId id)
    {
        Id = id;
        
    }

    public static Profile Create(UserId id)
    {
        return id.Value == Guid.Empty ? throw new DomainException("User ID is required") : new Profile(id);
    }
    
    public void AddSecondaryPosition(Position position)
    {
        var assignment = new PositionAssignment(position);

        if (_secondaryPositions.Contains(assignment)) return;

        _secondaryPositions.Add(assignment);
        MarkAsUpdated();
    }

    public void RemoveSecondaryPosition(Position position)
    {
        var assignment = new PositionAssignment(position);

        if (!_secondaryPositions.Remove(assignment)) return;

        MarkAsUpdated();
    }

    public void SetDisplayName(string displayName)
    {
        
        if (string.IsNullOrWhiteSpace(displayName))
        {
            throw new DomainException("Display name is required");
        }
        
        var normalizedName = displayName.Trim();
        
        if (DisplayName == normalizedName) return;
        
        DisplayName = normalizedName;
        
        MarkAsUpdated();
    }
    
}
