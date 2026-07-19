using Microsoft.AspNetCore.Diagnostics;
using OrderManagement.API.Handlers;
using OrderManagement.API.Middlewares;
using System.Net;
using System.Text.Json;

namespace OrderManagement.API.Extensions;

public static class ExceptionExtension
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder builder)
    {
        builder.UseExceptionHandler(error =>
        {
            error.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                IExceptionHandlerFeature? contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    Dictionary<string, object> exceptionProperties = ResponseHandler.Error(context.Response.StatusCode, "internal-server-error");
                    await context.Response.WriteAsync(JsonSerializer.Serialize(exceptionProperties));
                }
            });
        });
    }

    public static void ConfigureExceptionMiddleware(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ExceptionMiddleware>();
    }
}
