using FootballBuddy.Auth.Application.DTOs;
using FootballBuddy.Auth.Application.Exceptions;
using FootballBuddy.Auth.Application.Interfaces;
using FootballBuddy.Auth.Domain.Repositories;
using MediatR;

namespace FootballBuddy.Auth.Application.Commands.Login;

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDto>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IPasswordHasher _passwordHasher;

    public LoginCommandHandler(IPasswordHasher passwordHasher, IJwtTokenService jwtTokenService, IUsersRepository usersRepository)
    {
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
        _usersRepository = usersRepository;
    }

    public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLower();
        
        var user = await _usersRepository.GetUserByEmailAsync(email, cancellationToken);

        if (user is null)
        {
            throw new InvalidCredentialsException();
        }

        var isValidPassword = _passwordHasher.Verify(request.Password, user.PasswordHash);

        if (!isValidPassword)
        {
            throw new InvalidCredentialsException();
        }

        var token = _jwtTokenService.Generate(user, cancellationToken);
        
        

        return new AuthResponseDto()
        {
            AccessToken = token.AccessToken,
            ExpiresAt = token.ExpiresAt,
            User = new AuthUserDto()
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role.ToString()
            }
        };
    }
}