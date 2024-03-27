using Newtonsoft.Json;

namespace OrganizingEventsService.Presentation.Http.Middlewares;

public class RequestLoggerMiddleware : IMiddleware
{
    private readonly ILogger<RequestLoggerMiddleware> _logger;

    public RequestLoggerMiddleware(ILogger<RequestLoggerMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var loggedRequest = new
        {
            ip = context.Connection.RemoteIpAddress?.MapToIPv4() + ":" + context.Connection.RemotePort,
            method = context.Request.Method,
            endpoint = context.Request.Path
        };
        
        _logger.LogInformation($"Request: {loggedRequest.ip} {loggedRequest.method} {loggedRequest.endpoint}");
        await next(context);
    }
}