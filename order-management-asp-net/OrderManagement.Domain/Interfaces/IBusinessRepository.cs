using OrderManagement.Domain.Entities;

namespace OrderManagement.Domain.Interfaces;

public interface IBusinessRepository
{
    public Task<Business?> FindFirstAsync();
}
