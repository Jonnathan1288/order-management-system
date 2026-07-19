using OrderManagement.Application.Interfaces.Public;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Application.Services.Public;

public class PaymentMethodService(IPaymentMethodRepository _repository) : IPaymentMethodService
{
    public Task<List<PaymentMethod>> GetByBusinessAsync(short businessId)
    {
        return _repository.FindByBusinessAsync(businessId);
    }
}
