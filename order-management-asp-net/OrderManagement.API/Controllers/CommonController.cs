using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
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
            string value = GetTokenClaim("businessId") ?? Request.Headers["X-Business-Id"].ToString();

            if (!short.TryParse(value, out short businessId))
            {
                throw new BadHttpRequestException(
                    "businessId is mandatory in token or X-Business-Id header and must be numeric."
                );
            }

            return businessId;
        }
    }

    /// <summary>
    /// Get the user id from token or header.
    /// </summary>
    protected int UserId
    {
        get
        {
            string value = GetTokenClaim(ClaimTypes.Sid)
                ?? GetTokenClaim("sid")
                ?? GetTokenClaim("userId")
                ?? Request.Headers["X-User-Id"].ToString();

            if (!int.TryParse(value, out int userId))
            {
                throw new BadHttpRequestException(
                    "userId is mandatory in token or X-User-Id header and must be numeric."
                );
            }

            return userId;
        }
    }

    /// <summary>
    /// Get the customer id from token or header.
    /// </summary>
    protected int CustomerId
    {
        get
        {
            string value = GetTokenClaim("customerId") ?? Request.Headers["X-Customer-Id"].ToString();

            if (!int.TryParse(value, out int customerId))
            {
                throw new BadHttpRequestException(
                    "customerId is mandatory in token or X-Customer-Id header and must be numeric."
                );
            }

            return customerId;
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

        try
        {
            JwtSecurityTokenHandler handler = new();
            JwtSecurityToken jwtToken = handler.ReadJwtToken(Token);
            string? value = jwtToken.Claims.FirstOrDefault(claim => claim.Type == claimType)?.Value;
            return string.IsNullOrWhiteSpace(value) ? null : value;
        }
        catch
        {
            return null;
        }
    }
}
