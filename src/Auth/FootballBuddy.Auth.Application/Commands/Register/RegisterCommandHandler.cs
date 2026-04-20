using FootballBuddy.Auth.Application.DTOs;
using FootballBuddy.Auth.Application.Exceptions;
using FootballBuddy.Auth.Application.Interfaces;
using FootballBuddy.Auth.Domain.Aggregates;
using FootballBuddy.Auth.Domain.Repositories;
using MediatR;

namespace FootballBuddy.Auth.Application.Commands.Register;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IPasswordHasher _passwordHasher;
    
    public RegisterCommandHandler(IUsersRepository usersRepository, IJwtTokenService jwtTokenService, IPasswordHasher passwordHasher)
    {
        _usersRepository = usersRepository;
        _jwtTokenService = jwtTokenService;
        _passwordHasher = passwordHasher;
    }
    
    
    public async Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        var userExists = await _usersRepository.ExistsByEmailAsync(normalizedEmail, cancellationToken);
        if (userExists)
        {
            throw new EmailAlreadyInUseException(normalizedEmail);
        }
        
        var passwordHash = _passwordHasher.Hash(request.Password);
        var user = User.Create(normalizedEmail, passwordHash);
        await _usersRepository.AddAsync(user, cancellationToken);
        
        var token = _jwtTokenService.Generate(user, cancellationToken);

        return new AuthResponseDto
        {
            AccessToken = token.AccessToken,
            ExpiresAt = token.ExpiresAt,
            User = new AuthUserDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role.ToString()
            }
        };


    }
}