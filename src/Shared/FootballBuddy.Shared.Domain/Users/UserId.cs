namespace FootballBuddy.Shared.Domain.Users;

public readonly record struct UserId(Guid Value)
{
    public static UserId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
    
    public static implicit operator Guid(UserId id) => id.Value;
    public static explicit operator UserId(Guid id) => new(id);
}