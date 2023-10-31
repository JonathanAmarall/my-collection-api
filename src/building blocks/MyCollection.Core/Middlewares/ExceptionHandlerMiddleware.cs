using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MyCollection.Core.Exceptions;
using MyCollection.Core.Models;
using System.Net;
using System.Text.Json;

namespace MyCollection.Core.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;


    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Call the next delegate/middleware in the pipeline.
            await _next(context);
        }
        catch (PropertyQueryNullException ex)
        {
            _logger.LogError(ex, "An exception occurred: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// Handles the specified <see cref="Exception"/> for the specified <see cref="HttpContext"/>.
    /// </summary>
    /// <param name="httpContext">The HTTP httpContext.</param>
    /// <param name="exception">The exception.</param>
    /// <returns>The HTTP response that is modified based on the exception.</returns>
    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        (HttpStatusCode httpStatusCode, IReadOnlyCollection<string> errors) = GetHttpStatusCodeAndErrors(exception);

        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = httpStatusCode.GetHashCode();

        var serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        string response = JsonSerializer.Serialize(new ApiErrorResponse(errors.ToList()), serializerOptions);

        await httpContext.Response.WriteAsync(response);
    }

    private static (HttpStatusCode httpStatusCode, IReadOnlyCollection<string>) GetHttpStatusCodeAndErrors(Exception exception) =>
           exception switch
           {
               DomainException domainException => (HttpStatusCode.BadRequest, new[] { domainException.Error }),
               PropertyQueryNullException propertyQueryNullException => (HttpStatusCode.BadRequest, new[] { propertyQueryNullException.Message }),
               _ => (HttpStatusCode.InternalServerError, new[] { "Internal Server Error" })
           };
}