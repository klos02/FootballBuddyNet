using BuildingBlocks.Application.Abstractions;

namespace BuildingBlocks.Application;

public abstract class ApplicationException : Exception
{
    public abstract ErrorCodes ErrorCode { get; }
    protected ApplicationException(string message) : base(message)
    {
        
    }
}