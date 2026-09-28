using BuildingBlocks.Domain.Abstractions;
using BuildingBlocks.Domain.Exceptions;
using FootballBuddy.Profiles.Domain.Enums;
using FootballBuddy.Shared.Domain.Users;

namespace FootballBuddy.Profiles.Domain.Aggregates;

public sealed class Profile : AggregateRoot
{
    public UserId Id { get; private set; }
    public string? DisplayName { get; private set; }
    public Position? PreferredPosition { get; private set; }
    public IReadOnlyCollection<Position> SecondaryPositions => _secondaryPositions.AsReadOnly();
    
    
    
    private readonly List<Position> _secondaryPositions = new();
    
    
    private Profile() {}

    private Profile(UserId id)
    {
        Id = id;
        
    }

    public static Profile Create(UserId id)
    {
        return id.Value == Guid.Empty ? throw new DomainException("User ID is required") : new Profile(id);
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