using BuildingBlocks.Application.Abstractions;
using ApplicationException = BuildingBlocks.Application.ApplicationException;

namespace FootballBuddy.Auth.Application.Exceptions;

public class InvalidCredentialsException : ApplicationException

{
    public override ErrorCodes ErrorCode => ErrorCodes.Unauthorized;

    public InvalidCredentialsException() : base("Invalid email or password")
    {
        
    }
}