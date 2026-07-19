using OrderManagement.Domain.Entities;

namespace OrderManagement.Domain.Interfaces;

public interface IUserRepository
{
    public Task<User?> FindByBussinesAndEmailAsync(short businessId, string email);
}
