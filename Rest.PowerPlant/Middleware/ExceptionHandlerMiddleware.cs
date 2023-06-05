using Newtonsoft.Json;
using System.Net;

namespace Rest.PowerPlant.Middleware;

public class ExceptionHandlerMiddleware
{
    private Serilog.ILogger _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next, Serilog.ILogger logger)
    {
        _next = next;
        _logger = logger;
    }


    public async Task InvokeAsync(HttpContext httpContext)
    {

        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }

    }


    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var result = new ErrorDetails
        {
            StatusCode = context.Response.StatusCode,
            Message = exception.Message,
            Details = exception.ToString()
        };

        var json = JsonConvert.SerializeObject(result);
        _logger.Warning(exception, "An error occured during request processing");

        return context.Response.WriteAsync(json);
    }
}

internal class ErrorDetails
{
    public string Message { get; set; }
    public int StatusCode { get; set; }
    public string Details { get; set; }
}
