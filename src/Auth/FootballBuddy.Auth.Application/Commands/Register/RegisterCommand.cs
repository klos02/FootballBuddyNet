using FootballBuddy.Auth.Application.DTOs;
using MediatR;

namespace FootballBuddy.Auth.Application.Commands.Register;

public sealed record RegisterCommand(string Email, string Password) :IRequest<AuthResponseDto>;

    
