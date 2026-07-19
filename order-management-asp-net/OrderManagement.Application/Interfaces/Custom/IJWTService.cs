using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Interfaces.Custom;

public interface IJWTService
{
    public string GenerateToken(User user, int lifeTime = 3600);

    public string GetUserCode(string token);

    public long GetUserId(string token);

    public bool ValidateToken(string token);
}