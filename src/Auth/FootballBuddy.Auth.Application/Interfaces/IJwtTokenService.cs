using FootballBuddy.Auth.Application.DTOs;
using FootballBuddy.Auth.Domain.Aggregates;

namespace FootballBuddy.Auth.Application.Interfaces;

public interface IJwtTokenService
{
    JwtTokenResult Generate(User user, CancellationToken cancellationToken);
}