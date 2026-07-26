using Microsoft.AspNetCore.Mvc;
using OrderManagement.Domain.Enums.Custom;
using OrderManagement.Domain.Exceptions.Unauthorized;
using System.Security.Claims;

namespace OrderManagement.API.Controllers;

public class CommonController : ControllerBase
{
    /// <summary>
    /// Returns the current JWT Bearer for each request.
    /// </summary>
    protected string Token => Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

    protected string HostURL => $"{Request.Scheme}:://{Request.Host}";

    /// <summary>
    /// Get the business id from token or header.
    /// </summary>
    protected short BusinessId
    {
        get
        {
            return GetRequiredShortIdentifier("businessId", "X-Business-Id", "businessId");
        }
    }

    /// <summary>
    /// Get the user id from token or header.
    /// </summary>
    protected int UserId
    {
        get
        {
            return GetRequiredIntIdentifier("userId", "X-User-Id", ClaimTypes.Sid, "sid", "userId");
        }
    }

    /// <summary>
    /// Get the customer id from token or header.
    /// </summary>
    protected int CustomerId
    {
        get
        {
            return GetRequiredIntIdentifier("customerId", "X-Customer-Id", "customerId");
        }
    }

    /// <summary>
    /// Get the remote ip address.
    /// </summary>
    protected string RemoteIpAddress
    {
        get
        {
            return Convert.ToString(HttpContext.Request.Headers["X-Forwarded-For"]) ?? HttpContext.Connection.RemoteIpAddress?.ToString() ?? "";
        }
    }

    private string? GetTokenClaim(string claimType)
    {
        if (string.IsNullOrWhiteSpace(Token)) return null;

        if (HttpContext.User.Identity?.IsAuthenticated != true)
        {
            throw new UnauthorizedException(ExceptionEnum.InvalidToken);
        }

        string? value = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == claimType)?.Value;
        return string.IsNullOrWhiteSpace(value) ? null : value;
    }

    private short GetRequiredShortIdentifier(string fieldName, string headerName, params string[] claimTypes)
    {
        string value = GetIdentifierValue(headerName, claimTypes);

        if (!short.TryParse(value, out short identifier))
        {
            throw new BadHttpRequestException(
                $"{fieldName} is mandatory in token or {headerName} header and must be numeric."
            );
        }

        return identifier;
    }

    private int GetRequiredIntIdentifier(string fieldName, string headerName, params string[] claimTypes)
    {
        string value = GetIdentifierValue(headerName, claimTypes);

        if (!int.TryParse(value, out int identifier))
        {
            throw new BadHttpRequestException(
                $"{fieldName} is mandatory in token or {headerName} header and must be numeric."
            );
        }

        return identifier;
    }

    private string GetIdentifierValue(string headerName, params string[] claimTypes)
    {
        foreach (string claimType in claimTypes)
        {
            string? claimValue = GetTokenClaim(claimType);
            if (!string.IsNullOrWhiteSpace(claimValue)) return claimValue;
        }

        return Request.Headers[headerName].ToString();
    }
}
