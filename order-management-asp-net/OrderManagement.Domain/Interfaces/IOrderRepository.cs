using OrderManagement.Domain.Entities;

namespace OrderManagement.Domain.Interfaces;

public interface IOrderRepository
{
    Task<Order> CreateAsync(Order order);

    Task<List<Order>> FindByBusinessAsync(short businessId);

    Task<Order?> FindByBusinessAndCustomerAsync(short businessId, int customerId, int orderId);
}
