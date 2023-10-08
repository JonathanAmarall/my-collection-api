using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using MyCollection.Core.Exceptions;

namespace MyCollection.Core.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Call the next delegate/middleware in the pipeline.
            await _next(context);
        }
        catch (PropertyQueryNullException e)
        {
            Console.WriteLine(e);
            context.Response.StatusCode = StatusCodes.Status400BadRequest.GetHashCode();
            await context.Response.WriteAsJsonAsync(e.Message);
        }
    }
}