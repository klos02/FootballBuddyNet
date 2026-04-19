using FootballBuddy.Shared.Domain.Users;

namespace FootballBuddy.Auth.Application.DTOs;

public sealed class AuthUserDto
{
    public UserId Id { get; init; }
    public string Email { get; init; } = default!;
    public string Role { get; init; } = default!; 
}