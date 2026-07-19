
using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Interfaces.Public;

public interface IBusinessService
{
    public Task<Business?> GetFirstAsync();
}
