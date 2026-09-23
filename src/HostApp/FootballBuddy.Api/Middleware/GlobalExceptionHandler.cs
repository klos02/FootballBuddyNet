using BuildingBlocks.Application.Abstractions;
using BuildingBlocks.Domain.Exceptions;
using FootballBuddy.Auth.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ApplicationException = BuildingBlocks.Application.ApplicationException;

namespace FootballBuddy.Api.Middleware;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(
        IProblemDetailsService problemDetailsService,
        ILogger<GlobalExceptionHandler> logger)
    {
        _problemDetailsService = problemDetailsService;
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (httpContext.Response.HasStarted)
        {
            return false;
        }

        int statusCode;
        string title;

        switch (exception)
        {
            case ApplicationException applicationException:
                statusCode = MapStatusCode(applicationException.ErrorCode);
                title = "Application error";
                break;

            case DomainException:
                statusCode = StatusCodes.Status400BadRequest;
                title = "Domain rule violated";
                break;

            default:
                return false;
        }

        _logger.LogWarning(
            exception,
            "Handled {ExceptionType} with status {StatusCode}",
            exception.GetType().Name,
            statusCode);

        httpContext.Response.StatusCode = statusCode;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = exception.Message

        };

        if (exception is ValidationFailedException validationException)
        {
            problemDetails.Extensions["errors"] = validationException.Errors;
        }

        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = problemDetails
            
        });
    }

    private static int MapStatusCode(ErrorCodes errorCode) => errorCode switch
    {
        ErrorCodes.Unauthorized => StatusCodes.Status401Unauthorized,
        ErrorCodes.Conflict     => StatusCodes.Status409Conflict,
        ErrorCodes.Validation   => StatusCodes.Status400BadRequest,
        ErrorCodes.NotFound     => StatusCodes.Status404NotFound,
        _                       => StatusCodes.Status500InternalServerError
    };
}
