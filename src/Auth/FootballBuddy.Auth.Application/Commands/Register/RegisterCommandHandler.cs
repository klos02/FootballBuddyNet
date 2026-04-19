using FootballBuddy.Auth.Application.DTOs;
using MediatR;

namespace FootballBuddy.Auth.Application.Commands.Register;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
{
    
    
    
    public Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}