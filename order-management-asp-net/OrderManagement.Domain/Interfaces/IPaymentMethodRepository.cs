using OrderManagement.Domain.Entities;

namespace OrderManagement.Domain.Interfaces;

public interface IPaymentMethodRepository
{
    Task<List<PaymentMethod>> FindByBusinessAsync(short businessId);
}
