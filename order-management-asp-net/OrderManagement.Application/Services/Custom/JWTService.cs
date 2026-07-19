using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OrderManagement.Application.Interfaces.Custom;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Enums.Custom;
using OrderManagement.Domain.Exceptions.BadRequest;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrderManagement.Application.Services.Custom;

public class JWTService(IConfiguration configuration) : IJWTService
{
    private readonly string _audience = configuration["JWT:Audience"]!;

    private readonly string _issuer = configuration["JWT:Issuer"]!;

    private readonly string _key = configuration["JWT:Key"]!;

    public string GenerateToken(User user, int lifeTime = 3600)
    {
        List<Claim> claims =
        [
            new (ClaimTypes.Sid, user.Id.ToString()),
            new (ClaimTypes.NameIdentifier, user.Code),
            new (ClaimTypes.IsPersistent, "true"),
            new Claim(ClaimTypes.Email, user.Email),
            new("businessId", user.BusinessId.ToString()),
            new Claim(
                "customerId",
                user.Customers.FirstOrDefault()?.Id.ToString() ?? string.Empty
            )
        ];

        claims.Add(new Claim(ClaimTypes.Role, user.Role));

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_key));
        SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha512Signature);
        JwtSecurityToken token = new(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.Now.AddSeconds(lifeTime),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GetUserCode(string token)
    {
        return GetClaims(token).FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.NameIdentifier))?.Value ?? "";
    }

    public bool ValidateToken(string token)
    {

        try
        {
            _ = long.TryParse(GetClaims(token).FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.Sid))?.Value, out long userId);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public int GetUserId(string token)
    {
        try
        {
            _ = int.TryParse(GetClaims(token).FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.Sid))?.Value, out int userId);
            return userId;
        }
        catch
        {
            throw new BadRequestException(ExceptionEnum.ExpiredToken);
        }
    }

    #region This not part of interface
    private List<Claim> GetClaims(string token)
    {
        JwtSecurityTokenHandler handler = new();
        ClaimsPrincipal claimsPrincipal = handler.ValidateToken(token, new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _issuer,
            ValidAudience = _audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)),
        }, out _);
        return claimsPrincipal.Claims.ToList();
    }
    #endregion
}

