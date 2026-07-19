using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Application.Interfaces.Public;

namespace OrderManagement.Application.Services.Public;

public class BusinessService(
    IBusinessRepository _repository) : IBusinessService
{
    public Task<Business?> GetFirstAsync() {
        return _repository.FindFirstAsync();
    }

}