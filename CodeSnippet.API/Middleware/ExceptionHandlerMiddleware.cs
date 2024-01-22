using CodeSnippet.Domain.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace CodeSnippet.API.Middleware;

public class ExceptionHandlerMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

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

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() },
            Formatting = Newtonsoft.Json.Formatting.Indented
        };

        return context.Response.WriteAsync(
            JsonConvert.SerializeObject(
                new ApiResult(
                    success: false,
                    message: $"An error occurred: {exception.Message}",
                    result: new { }),
                jsonSettings));
    }
}

/// <summary>
/// Contains extension methods for configuring the exception handler middleware.
/// </summary>
public static class ExceptionHandlerMiddlewareExtensions
{
    /// <summary>
    /// Configure the custom exception handler middleware.
    /// </summary>
    /// <param name="builder">The application builder.</param>
    /// <returns>The configured application builder.</returns>
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder) => builder.UseMiddleware<ExceptionHandlerMiddleware>();
}
