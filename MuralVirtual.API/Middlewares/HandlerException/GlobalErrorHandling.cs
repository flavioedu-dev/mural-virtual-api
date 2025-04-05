using MuralVirtual.Domain.Exceptions;
using System.Text.Json;

namespace MuralVirtual.API.Middlewares.HandlerException;

public class GlobalErrorHandling
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalErrorHandling> _logger;

    public GlobalErrorHandling(RequestDelegate next, ILogger<GlobalErrorHandling> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (CustomResponseException customException)
        {
            await HandleExceptionAsync(httpContext, customException, customException?.StatusCode ?? 400);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex, StatusCodes.Status400BadRequest);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception, int statusCode)
    {
        _logger.LogError($"Error Message: {exception.Message}");
        _logger.LogError($"Inner Error Message: {exception.InnerException?.Message}");
        _logger.LogError($"Error Stack: {exception.StackTrace}");

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;

        var message = statusCode == 401 ? "ApplicationMessages.Authentication_Login_Credentials_Invalid" : exception.InnerException?.Message ?? exception.Message;
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new { errorMessage = message }));
    }
}