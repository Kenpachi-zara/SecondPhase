using SecondPhase.Services;

namespace SecondPhase.Middleware;

public class MyMiddleware(
    RequestDelegate next,
    ILogger<MyMiddleware> logger,
    ISingletonServiceExample singletonService)
{
    private readonly ILogger _logger = logger;

    public async Task InvokeAsync(HttpContext context,
        ITransientServiceExample transientService, IScopedServiceExample scopedService)
    {
        logger.LogInformation("MyMiddleware called");
        transientService.LogMessageGuid();
        scopedService.LogMessageGuid();
        singletonService.LogMessageGuid();
        logger.LogInformation("MyMiddleware ended");
        await next(context);
    }
}

public static class MyMiddlewareExtensions
{
    public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder) =>
        builder.UseMiddleware<MyMiddleware>();
}