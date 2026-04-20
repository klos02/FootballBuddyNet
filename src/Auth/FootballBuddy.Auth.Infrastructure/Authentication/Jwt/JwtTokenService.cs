using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FootballBuddy.Auth.Application.DTOs;
using FootballBuddy.Auth.Application.Interfaces;
using FootballBuddy.Auth.Domain.Aggregates;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FootballBuddy.Auth.Infrastructure.Authentication.Jwt;

public class JwtTokenService : IJwtTokenService
{
    private readonly JwtOptions _jwtOptions;

    public JwtTokenService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public JwtTokenResult Generate(User user, CancellationToken cancellationToken)
    {
        var expiresAtUtc = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationMinutes);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.Value.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role.ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: expiresAtUtc,
            signingCredentials: credentials
        );
        
        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

        return new JwtTokenResult
        {
            AccessToken = accessToken,
            ExpiresAt = expiresAtUtc,
        };


    }
}