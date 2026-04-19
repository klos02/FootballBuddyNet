using BuildingBlocks.Domain.Abstractions;
using BuildingBlocks.Domain.Exceptions;
using FootballBuddy.Auth.Domain.Enums;
using FootballBuddy.Auth.Domain.Events;
using FootballBuddy.Shared.Domain.Users;

namespace FootballBuddy.Auth.Domain.Aggregates;

public class User : AggregateRoot
{
    public UserId Id { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public UserRole Role { get; private set; }
    
    private User() {}


    private User(UserId id, string email, string passwordHash, UserRole role)
    {
        Id = id;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
    }

    public static User Create(string email, string passwordHash)
    {
        if(string.IsNullOrEmpty(email)) throw new DomainException("Email is required");
        if(string.IsNullOrEmpty(passwordHash)) throw new DomainException("PasswordHash is required");
        
        var user = new User(
            UserId.New(),
            email,
            passwordHash,
            UserRole.User);
        
        user.RaiseDomainEvent(new UserRegisteredDomainEvent(user.Id));
        
        return user;
    }
}