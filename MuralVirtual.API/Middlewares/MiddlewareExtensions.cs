using MuralVirtual.API.Middlewares.HandlerException;

namespace MuralVirtual.API.Middlewares;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder GlobalErrorHandling(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalErrorHandling>();
    }
}
