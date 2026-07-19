using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Interfaces.Public;

public interface IPaymentMethodService
{
    Task<List<PaymentMethod>> GetByBusinessAsync(short businessId);
}
