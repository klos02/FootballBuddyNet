using FluentValidation;

namespace FootballBuddy.Auth.Application.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email).NotNull().NotEmpty();
        RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(8);
    }
}