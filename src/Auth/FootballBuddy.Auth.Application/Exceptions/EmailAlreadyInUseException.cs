using BuildingBlocks.Application.Abstractions;
using ApplicationException = BuildingBlocks.Application.ApplicationException;

namespace FootballBuddy.Auth.Application.Exceptions;

public class EmailAlreadyInUseException : ApplicationException
{
    public string Email { get; }

    public override ErrorCodes ErrorCode => ErrorCodes.Conflict;

    public EmailAlreadyInUseException(string email) : base($"Email {email} is already in use")
    {
        Email = email;
    }
}