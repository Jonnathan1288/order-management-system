using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Interfaces.Public;

public interface IOrderService
{
    Task<Order> CreateAsync(short businessId, Order order);

    Task<List<Order>> GetByBusinessAsync(short businessId);

    Task<Order?> GetByBusinessAndCustomerAsync(short businessId, int customerId, int orderId);
}
