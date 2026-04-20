namespace FootballBuddy.Auth.Application.DTOs;

public sealed class JwtTokenResult
{
    public string AccessToken { get; init; } = default!;
    public DateTime ExpiresAt { get; init; }
}