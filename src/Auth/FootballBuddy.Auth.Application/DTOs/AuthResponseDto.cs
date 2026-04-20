

namespace FootballBuddy.Auth.Application.DTOs;

public sealed class AuthResponseDto
{
    public string AccessToken {get; init;}
    public DateTime ExpiresAt {get; init;}
    public AuthUserDto User {get; init;}
}