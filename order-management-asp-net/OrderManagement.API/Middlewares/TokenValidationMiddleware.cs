using Microsoft.AspNetCore.Mvc;
using OrderManagement.API.Attributes;
using OrderManagement.Application.Interfaces.Custom;
using OrderManagement.Application.Interfaces.Public;
using System.Security.Claims;
using System.Text.Json;

namespace OrderManagement.API.Middlewares;

public class TokenValidationMiddleware(RequestDelegate _next)
{
    public async Task InvokeAsync(
        HttpContext context,
        [FromServices] IUserService authService,
        [FromServices] IRequestContextService requestContextService)
    {
        JsonSerializerOptions jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        if (context.GetEndpoint()?.Metadata.GetMetadata<SkipTokenValidationAttribute>() != null)
        {
            await _next(context);
            return;
        }

        var authorizationHeader = context.Request.Headers.Authorization.ToString();

        string token = authorizationHeader.Replace("Bearer ", "");

        var user = await authService.VerifyToken(token);

        requestContextService.User = user;

        context.Request.Headers.Append("X-User-Code", user.Code);
        context.Request.Headers.Append("X-User-Id", user.Id.ToString());

        List<Claim> claims =
        [
            new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Code),
            ];

        var identity = new ClaimsIdentity(claims, "custom");
        context.User = new ClaimsPrincipal(identity);
        await _next(context);
    }
}

