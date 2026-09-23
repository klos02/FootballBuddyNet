using BuildingBlocks.Application.Abstractions;
using ApplicationException = BuildingBlocks.Application.ApplicationException;

namespace FootballBuddy.Auth.Application.Exceptions;

public class ValidationFailedException : ApplicationException
{
    public IReadOnlyDictionary<string, string[]> Errors { get; }

    public override ErrorCodes ErrorCode => ErrorCodes.Validation;
    
    public ValidationFailedException(IReadOnlyDictionary<string, string[]> errors)
        : base("One or more validation errors occurred.")
    {
        Errors = errors;
    }
}