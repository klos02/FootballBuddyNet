using FootballBuddy.Auth.Application.DTOs;
using MediatR;

namespace FootballBuddy.Auth.Application.Commands.Login;

public sealed record LoginCommand(string Email, string Password) : IRequest<AuthResponseDto>;