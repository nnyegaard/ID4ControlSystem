using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LEGO4IDControl.Infrastructure;

public class NoOrderFoundException : Exception
{
    public NoOrderFoundException(string message) : base(message){}
}


public class CustomExceptionHandler : IExceptionHandler
{
    private readonly ILogger<CustomExceptionHandler> _logger;

    public CustomExceptionHandler(ILogger<CustomExceptionHandler> logger)
    {
        _logger = logger;
    }
    
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError($"An error occurred while processing your request: {exception.Message}");
      
        var problemDetails = new ProblemDetails
        {
            Status = (int)HttpStatusCode.InternalServerError,
            Type = exception.GetType().Name,
            Title = "An unhandled error occurred",
            Detail = exception.Message
        };

        switch (exception)
        {
            case NoOrderFoundException:
                problemDetails.Status = 404;
                problemDetails.Title = "Order was not found";
                problemDetails.Detail = exception.Message; 
                break;
        }

        httpContext.Response.StatusCode = (int)problemDetails.Status;
        await httpContext
            .Response
            .WriteAsJsonAsync(problemDetails.Detail, cancellationToken);
        
        return true;
    }
}