using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

public class ErrorHandlerMiddleware
{

    private readonly RequestDelegate _next;
    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);

        }

        catch (InvalidIDException)
        {

            Console.WriteLine("Invalid Id.");
            context.Response.StatusCode = 404;
        }
    }
}
public static class MyMiddlewareExtensions
{

    public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlerMiddleware>();


    }
}