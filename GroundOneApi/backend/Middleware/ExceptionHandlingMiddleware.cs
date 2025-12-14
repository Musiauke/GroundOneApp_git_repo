using System.Net;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace backend.Middleware;

/// <summary>
/// Global Exception Handler - it catches unhandled exceptions and returns standardized error responses
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Log and error
        _logger.LogError(exception, 
            "An unhandled exception occurred. Path: {Path}, Method: {Method}", 
            context.Request.Path, 
            context.Request.Method);

        // determine status code and message
        var (statusCode, message) = exception switch
        {
            ArgumentNullException => (HttpStatusCode.BadRequest, "Wymagany parametr jest pusty"),
            ArgumentException => (HttpStatusCode.BadRequest, exception.Message),
            KeyNotFoundException => (HttpStatusCode.NotFound, "Zasób nie został znaleziony"),
            UnauthorizedAccessException => (HttpStatusCode.Unauthorized, "Brak dostępu"),
            InvalidOperationException => (HttpStatusCode.BadRequest, exception.Message),
            DbUpdateConcurrencyException => (HttpStatusCode.Conflict, "Konflikt wersji danych"),
            DbUpdateException => (HttpStatusCode.BadRequest, "Błąd aktualizacji bazy danych"),
            _ => (HttpStatusCode.InternalServerError, "Wystąpił nieoczekiwany błąd")
        };

        // prepare error response
        var response = new ErrorResponse
        {
            StatusCode = (int)statusCode,
            Message = message,
            Details = context.Request.Path,
            Timestamp = DateTime.UtcNow
        };

        // ad error details in development
        var environment = context.RequestServices.GetRequiredService<IWebHostEnvironment>();
        if (environment.IsDevelopment())
        {
            response.DeveloperMessage = exception.Message;
            response.StackTrace = exception.StackTrace;
        }

        // set response
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });

        await context.Response.WriteAsync(json);
    }
}

/// <summary>
/// standardized error response structure   
/// </summary>
public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    
    // only in development
    public string? DeveloperMessage { get; set; }
    public string? StackTrace { get; set; }
}