using CodeInk.API.Errors;
using CodeInk.Core.Exceptions;
using System.Net;

namespace CodeInk.API.Middlewares;

public class GlobalErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;
    private readonly IHostEnvironment _hostEnvironment;

    public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger, IHostEnvironment hostEnvironment)
    {
        _next = next;
        _logger = logger;
        _hostEnvironment = hostEnvironment;
    }


    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);

            if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
                await HandleNotFoundEndPointAsync(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            await HandleExceptionAsync(context, ex);

        }
    }

    private async Task HandleNotFoundEndPointAsync(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        var response = new ErrorDetails((int)HttpStatusCode.NotFound,
                                        $"The End Point {context.Request.Path} Not Found.");

        await context.Response.WriteAsync(response.ToString());
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";


        var response = new ErrorDetails()
        {
            ErrorMessage = ex.Message,
        };


        context.Response.StatusCode = ex switch
        {
            NotFoundException => (int)HttpStatusCode.NotFound,
            BadRequestException => (int)HttpStatusCode.BadRequest,
            UnAuthorizedException => (int)HttpStatusCode.Unauthorized,
            ValidationException validationException => HandleValidationException(validationException, response),
            _ => (int)HttpStatusCode.InternalServerError,
        };

        response.StatusCode = context.Response.StatusCode;

        await context.Response.WriteAsync(response.ToString());
    }

    private int HandleValidationException(ValidationException ex, ErrorDetails response)
    {
        response.Errors = ex.Errors;
        return (int)HttpStatusCode.BadRequest;

    }
}
